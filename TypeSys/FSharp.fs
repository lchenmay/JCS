module TypeSys.FSharp

open System
open System.Collections.Generic
open System.Text

open Util.Text
open Util.FileSys

let reserves = 
    [|  "let"
        "module"
        "mutable"
        "namespace"
        "of"
        "open"
        "ref"
        "type"
        "use" |]

let symbols = 
    [|  "."
        "<-"
        "|"
        "|>"
        "("
        ")"
        "{"
        "}"
        "[]"
        "[|"
        "|]"
        "="
        ":" |]

type Token = 
| Reserved of string
| Identifier of string
| Symbol of string
| Quoted of string
| Literal of string
| Undefined

type Line = {
src: string
tokens: Token[]
indent: int }

type FSharpCompiler = {
output: string -> unit }

let lex txt = 

    [| |]

let txt__line (txt:string) = 

    let trim = txt.TrimStart()

    {   src = txt
        tokens = lex txt
        indent = txt.Length - trim.Length }

let removeComment = Array.map(fun (i:string) -> 
    let index = i.IndexOf "//"
    if index >= 0 then
        i.Substring(0,index)
    else
        i)

let go output file = 
    
    let compiler = {
        output = output }

    let lines = 
        let error,lines = file |> try_read_lines
        lines
        |> removeComment
        |> Array.map txt__line

    ()

