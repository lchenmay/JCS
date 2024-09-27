module TypeSys.FrontendPackVue

open System
open System.Text
open System.IO

open Util.Text
open Util.Json

open TypeSys.MetaType
open TypeSys.Common
open TypeSys.CodeRobotI

let buildComponent jsPath json = 

    let n = json |> tryFindStrByAtt "name"
    let props = 
        match json |> tryFindByAtt "props" with
        | Some (n,j) ->
            match j with
            | Json.Braket items -> items
            | _ -> [| |]
        | None -> [| |]
    let states = 
        match json |> tryFindByAtt "states" with
        | Some (n,j) ->
            match j with
            | Json.Braket items -> items
            | _ -> [| |]
        | None -> [| |]

    let filename = Path.Combine(jsPath,"src","pages",n + ".vue")

    let w = empty__TextBlockWriter()

    "<template>" |> w.newline
    "</template>" |> w.newline
    
    [|  ""
        "<script setup lang=\"ts\">"
        ""
        "import { glib } from '~/lib/glib'"
        "import * as Common from '~/lib/store/common'"
        "" |]
    |> w.multiLine

    if props.Length > 0 then
        "const props = defineProps([" |> w.newline
        props
        |> Array.map(fun (n,j) -> "'" + n + "'")
        |> String.concat ","
        |> w.appendEnd
        "])" |> w.appendEnd
    props
    |> Array.map(fun (n,j) -> 
        let t = 
            match j with
            | Json.Str s -> s
            | _ -> ""
        "props." + n + " as " + t)
    |> w.multiLine

    w.newlineBlank()

    if states.Length > 0 then
        "const s = glib.vue.reactive({" |> w.newline
        states
        |> Array.map(fun (n,j) -> 
            let t = 
                match j with
                | Json.Str s -> s
                | _ -> ""
            "" + n + ": " + t)
        |> w.multiLine
        "})" |> w.newline

    [|  ""
        "glib.vue.onMounted(async () => {"
        "})"
        ""
        "</script>" |]
    |> w.multiLine

    //w.concat crlf
    //|> Util.FileSys.try_write_text filename
    //|> ignore
    
    ()

let buildComponents jsPath items = 
    items
    |> Array.iter(fun i -> 
        let n = i |> tryFindStrByAtt "name"
        buildComponent jsPath i)

let build designPath jsPath = 

    match
        Path.Combine(designPath,"Design.json")
        |> Util.FileSys.try_read_string 
        |> snd
        |> str__root
        |> tryFindByAtt "vue" with
    | Some (n,vue) -> 
        vue
        |> tryFindAryByAtt "components"
        |> buildComponents jsPath
    | None -> ()
