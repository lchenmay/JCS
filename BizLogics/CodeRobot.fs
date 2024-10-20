module BizLogics.CodeRobot

open System
open System.Text
open System.IO
open System.Collections.Generic
open System.Threading

open Util.Cat
open Util.Text
open Util.Bin
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

let compFile() = 
    let w = empty__TextBlockWriter()
    [|  "<template>"
        "</template>"
        ""
        """<script setup lang="ts">"""
        ""
        "import { glib } from '~/lib/glib'"
        ""
        "</script>"  |]
    |> w.multiLine

    w.Buffer().ToArray()
    
let buildComps pc (hostconfig:HOSTCONFIG) = 

    let path = hostconfig.p.DirVsCodeWeb + "/src/comps"
    Util.FileSys.checkpath path |> ignore

    pc.comps.Values
    |> Array.iter(fun i -> 
        let f = path + i.comp.p.Name + ".vue"

        let path = Directory.GetParent f
        Util.FileSys.checkpath path.FullName |> ignore

        File.WriteAllLines(f,compFile())

        ())

type VueFile = {
mutable template: string[]
mutable imports: string[] }

let buildVueFile() = 
    {   template =     
            [|  "<template>"
                "</template>" |]

        imports =    
            [|  "import { glib } from '~/lib/glib'"
                "import * as Common from '~/lib/store/common'" |] }

let VueFile__src vueFile = 
    let res = new List<string>()

    vueFile.template |> res.AddRange
    "" |> res.Add
    """<script setup lang="ts">""" |> res.Add
    "" |> res.Add
    vueFile.imports |> res.AddRange
    "" |> res.Add
    "</script>" |> res.Add

    res.ToArray()
    
let tryTake (lines:string[]) (head:string) (rear:string) =
    let i1 = lines |> Array.tryFindIndex(fun line -> line.StartsWith head)
    let i2 = lines |> Array.tryFindIndexBack(fun line -> line.StartsWith rear)

    if i1.IsSome && i2.IsSome then
        Array.sub lines i1.Value (i2.Value - i1.Value + 1)
    else
        [| |]

let src__VueFile (lines:string[]) = 
    let res = buildVueFile()

    let template = tryTake lines "<template>" "</template>"
    if template.Length > 0 then
        res.template <- template

    let imports = tryTake lines "imports " "imports "
    if imports.Length > 0 then
        res.imports <- imports

    res

let buildPages pc (hostconfig:HOSTCONFIG) = 

    let path = hostconfig.p.DirVsCodeWeb + "/src/pages"
    Util.FileSys.checkpath path |> ignore

    pc.pages.Values
    |> Array.iter(fun i -> 
        let f = path + i.page.p.Name + ".vue"

        let path = Directory.GetParent f
        Util.FileSys.checkpath path.FullName |> ignore

        let vueFile = 
            if File.Exists f then
                File.ReadAllLines f
                |> src__VueFile
            else
                buildVueFile()

        File.WriteAllLines(f,VueFile__src vueFile)

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
