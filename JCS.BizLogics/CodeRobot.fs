module JCS.BizLogics.CodeRobot

open System
open System.Text
open System.IO
open System.Collections.Generic
open System.Threading

open Util.Cat
open Util.Text
open Util.Bin
open Util.CollectionModDict
open Util.Perf
open Util.Json
open Util.FileSys
open Util.Http
open Util.HttpServer
open Util.Zmq

open UtilWebServer.Common
open UtilWebServer.Api
open UtilWebServer.Json
open UtilWebServer.SSR

open JCS.Shared.OrmTypes
open JCS.Shared.Types
open JCS.Shared.OrmMor
open JCS.Shared.CustomMor

open JCS.BizLogics.Common
open JCS.BizLogics.Branch

type VueFile = {
mutable template: string[]
mutable imports: string[]
mutable states: string[]
mutable onMounted: string[] }

let buildVueFile projectx = 
    {   template =     
            [|  "<template>"
                "</template>" |]

        imports =    
            [|  "import { glib } from '~/lib/glib'"
                "import { useRoute } from 'vue-router'"
                "import * as Common from '~/lib/store/common'" |]

        states =    
            [|  "const s = glib.vue.reactive({"
                "query: useRoute().query,"
                "user: runtime.user," 
                "data: runtime.data"
                "})" |]
                
        onMounted =    
            [|  "glib.vue.onMounted(async () => {"
                "})" |] }

let VueFile__src vueFile projectx (props: ModDictStr<VARTYPE>) = 
    let res = new List<string>()

    vueFile.template |> res.AddRange
    "" |> res.Add
    """<script setup lang="ts">""" |> res.Add
    "" |> res.Add
    vueFile.imports |> res.AddRange
    "" |> res.Add

    if props.count > 0 then
        let vs = props.Values
        let names = vs |> Array.map(fun i -> "'" + i.p.Name + "'") |> String.concat ","
        "const props = defineProps([" + names + "])" |> res.Add
        vs
        |> Array.iter(fun i -> 
            let typeAnnotation = 
                match i.p.Type with
                | "number"
                | "bool"
                | "string" -> i.p.Type
                | _ -> projectx.project.p.Code.ToLower() + "." + i.p.Type
            "props." + i.p.Name + " as " + typeAnnotation
            |> res.Add)
    "" |> res.Add

    vueFile.states |> res.AddRange
    "" |> res.Add
    vueFile.onMounted |> res.AddRange
    "" |> res.Add
    "</script>" |> res.Add

    res.ToArray()
    
let src__VueFile projectx (lines:string[]) = 
    let res = buildVueFile projectx

    let template = tryTake lines ("<template>","</template>") true
    if template.Length > 0 then
        res.template <- template

    let imports = tryTake lines ("import ","import ") true
    if imports.Length > 0 then
        res.imports <- imports

    let states = tryTake lines ("const s = glib.vue.reactive({","})") false
    if states.Length > 0 then
        res.states <- states
    
    let onMounted = tryTake lines ("glib.vue.onMounted(async () => {","})") false
    if onMounted.Length > 0 then
        res.onMounted <- onMounted

    res

let buildComps projectx (hostconfig:HOSTCONFIG) = 

    let path = hostconfig.p.DirVsCodeWeb + "/src/comps"
    Util.FileSys.checkpath path |> ignore

    projectx.compxs.Values
    |> Array.iter(fun compx -> 
        let f = path + compx.comp.p.Name + ".vue"

        let path = Directory.GetParent f
        Util.FileSys.checkpath path.FullName |> ignore

        let vueFile = 
            if File.Exists f then
                File.ReadAllLines f
                |> src__VueFile projectx
            else
                buildVueFile projectx

        compx.states.Values
        |> Array.iter(fun state ->
            match
                vueFile.states
                |> Array.tryFind(fun line -> line.StartsWith (state.p.Name + ":")) with
            | Some line -> ()
            | None -> 
                let ls = new List<string>(vueFile.states)
                ls.Insert(1,state.p.Name + ": " + state.p.Val + ",")
                vueFile.states <- ls.ToArray())

        File.WriteAllLines(f,VueFile__src vueFile projectx compx.props)

        ())

let buildPages projectx (hostconfig:HOSTCONFIG) = 

    let path = hostconfig.p.DirVsCodeWeb + "/src/pages"
    Util.FileSys.checkpath path |> ignore

    projectx.pagexs.Values
    |> Array.iter(fun pagex -> 
        let f = path + pagex.page.p.Name + ".vue"

        let path = Directory.GetParent f
        Util.FileSys.checkpath path.FullName |> ignore

        let vueFile = 
            if File.Exists f then
                File.ReadAllLines f
                |> src__VueFile projectx
            else
                buildVueFile projectx

        File.WriteAllLines(f,VueFile__src vueFile projectx pagex.props)

        ())

let changeFile path changer = 
    if File.Exists path then
        let txt = 
            File.ReadAllText path
            |> changer
        File.WriteAllText(path,txt)

let runProject projectx = 

    let hostconfig = 
        projectx.hostconfigs.Values
        |> Array.find(fun i -> i.p.Hostname.ToUpper() = System.Environment.MachineName.ToUpper())

    buildComps projectx hostconfig
    buildPages projectx hostconfig

    let ns = projectx.project.p.Code.ToLower()

    (fun (src:string) -> 
        let mutable res = src
        res <- res.Replace("var clientRuntime: [].ClientRuntime","var clientRuntime: " + ns + ".ClientRuntime")
        res <- res.Replace("user: [].[]","user: " + ns + "." + projectx.project.p.TypeSessionUser)
        res <- res.Replace("data: [].ClientRuntime","data: " + ns + ".ClientRuntime")
        res)
    |> changeFile(hostconfig.p.DirVsCodeWeb + "/src/types/main.d.ts")

    (fun (src:string) -> 
        let mutable res = src
        res <- res.Replace("[].MomentComplex",ns + ".MomentComplex")
        res)
    |> changeFile(hostconfig.p.DirVsCodeWeb + "/src/comps/RichTextEditor.vue")

    (fun (src:string) -> 
        let mutable res = src
        res <- res.Replace("[].",ns + ".")
        res)
    |> changeFile(hostconfig.p.DirVsCodeWeb + "/src/comps/Uploader.vue")

    (fun (src:string) -> 
        let mutable res = src
        res <- res.Replace("JSON.parse(localUser) as [].[]","JSON.parse(localUser) as " + ns + "." + projectx.project.p.TypeSessionUser)
        res <- res.Replace("runtime.user = glib.Mor.[]_empty()","runtime.user = glib.Mor." + ns + "." + projectx.project.p.TypeSessionUser + "_empty()")
        res)
    |> changeFile(hostconfig.p.DirVsCodeWeb + "/src/main.ts")

    (fun (src:string) -> 
        src.Replace("[]: { ...cm, ...om }",ns + ": { ...cm, ...om }"))
    |> changeFile(hostconfig.p.DirVsCodeWeb + "/src/lib/glib.ts")
    

let runAll exeDir =

    JCS.BizLogics.Init.init runtime

    [|  
        234346L // JCS
        234351L // FA
        234352L // JA
        234347L // Game
        234348L // J
        234350L // Studio
        234349L // J7
                |] 
    |> Array.map(fun id -> runtime.data.projectxs[id])
    |> Array.iter runProject

