module BizLogics.CodeRobot

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

open Shared.OrmTypes
open Shared.Types
open Shared.OrmMor
open Shared.CustomMor

open UtilWebServer.Common
open UtilWebServer.Api
open UtilWebServer.Json
open UtilWebServer.SSR

open Shared.OrmTypes
open Shared.OrmMor
open Shared.Types
open Shared.CustomMor

open BizLogics.Common
open BizLogics.Branch

let conn = "server=127.0.0.1; user=sa; database=JCS"
let mainDir = @"C:\Dev\JCS\Shared"
let JsDir = @"C:\Dev\JCS\vscode\src\lib\shared"
    

type VueFile = {
mutable template: string[]
mutable imports: string[]
mutable states: string[]
mutable onMounted: string[] }

let buildVueFile() = 
    {   template =     
            [|  "<template>"
                "</template>" |]

        imports =    
            [|  "import { glib } from '~/lib/glib'"
                "import * as Common from '~/lib/store/common'" |]

        states =    
            [|  "const s = glib.vue.reactive({"
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
            "props." + i.p.Name + " as " + projectx.project.p.Code.ToLower() + "." + i.p.Type
            |> res.Add)
    "" |> res.Add

    vueFile.states |> res.AddRange
    "" |> res.Add
    vueFile.onMounted |> res.AddRange
    "" |> res.Add
    "</script>" |> res.Add

    res.ToArray()
    
let src__VueFile (lines:string[]) = 
    let res = buildVueFile()

    let template = tryTake lines ("<template>","</template>") true
    if template.Length > 0 then
        res.template <- template

    let imports = tryTake lines ("imports ","imports ") true
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

    projectx.comps.Values
    |> Array.iter(fun compx -> 
        let f = path + compx.comp.p.Name + ".vue"

        let path = Directory.GetParent f
        Util.FileSys.checkpath path.FullName |> ignore

        let vueFile = 
            if File.Exists f then
                File.ReadAllLines f
                |> src__VueFile
            else
                buildVueFile()

        compx.states.Values
        |> Array.iter(fun prop ->
            match
                vueFile.states
                |> Array.tryFind(fun line -> line.StartsWith (prop.p.Name + ":")) with
            | Some line -> ()
            | None -> 
                let ls = new List<string>(vueFile.states)
                ls.Insert(1,prop.p.Name + ":" + projectx.project.p.Code.ToLower() + "." + prop.p.Type + ",")
                vueFile.states <- ls.ToArray())

        File.WriteAllLines(f,VueFile__src vueFile projectx compx.props)

        ())

let buildPages projectx (hostconfig:HOSTCONFIG) = 

    let path = hostconfig.p.DirVsCodeWeb + "/src/pages"
    Util.FileSys.checkpath path |> ignore

    projectx.pages.Values
    |> Array.iter(fun pagex -> 
        let f = path + pagex.page.p.Name + ".vue"

        let path = Directory.GetParent f
        Util.FileSys.checkpath path.FullName |> ignore

        let vueFile = 
            if File.Exists f then
                File.ReadAllLines f
                |> src__VueFile
            else
                buildVueFile()

        File.WriteAllLines(f,VueFile__src vueFile projectx pagex.props)

        ())

let run() =

    BizLogics.Init.init runtime

    let pc = runtime.data.pcs[234346L]

    let hostconfig = 
        pc.hostconfigs.Values
        |> Array.find(fun i -> i.p.Hostname.ToUpper() = System.Environment.MachineName.ToUpper())

    buildComps pc hostconfig
    buildPages pc hostconfig

    ()
