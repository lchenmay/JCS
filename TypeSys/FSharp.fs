module TypeSys.FSharp

open System
open System.Collections.Generic
open System.Text
open System.Text.RegularExpressions

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
| Newline of int // int = indent
| Comment of string
| Reserved of string
| Identifier of string
| Symbol of string
| Quoted of string
| Literal of string
| Undefined of string

type LexState = 
| Normal
| Quoted
| Comment


let regex_options = RegexOptions.Multiline ||| RegexOptions.ExplicitCapture ||| RegexOptions.Compiled

let string__regex s = new Regex(s,regex_options)

let r0 = 
    // (* """ " // 
    @"(\(\*|0x220x220x22|0x22|//)" 
    |> string__regex

let mask (src:string) (tokens:List<Token>) = 

    let index = ref 0

    let maskNext 
        (index:int ref)
        (src:string) 
        (pattern:string) 
        (m:System.Text.RegularExpressions.Match) 
        wrapper = 

        let e = 
            let starting = m.Index + m.Value.Length
            let ending = src.IndexOf(pattern,starting)
            if ending >= starting then
                ending + pattern.Length
            else
                src.Length - 1

        index.Value <- e

        src.Substring(m.Index,e - m.Index) 
        |> wrapper
        |> tokens.Add

    while index.Value < src.Length do
        let m = r0.Match(src,index.Value)
        if m.Success then
            if m.Index > index.Value then
                src.Substring(index.Value, m.Index - index.Value) 
                |> Token.Undefined
                |> tokens.Add
            match m.Value with
            | "(*" -> maskNext index src "*)" m Token.Comment
            | "\"\"\"" -> maskNext index src "\"\"\"" m Token.Quoted
            | "\"" -> maskNext index src "\"" m Token.Quoted
            | "//" -> maskNext index src crlf m Token.Comment
            | _ -> ()
        else
            src.Substring(index.Value, src.Length - index.Value) 
            |> Token.Undefined
            |> tokens.Add
            index.Value <- src.Length

    let items = tokens.ToArray()

    tokens.Clear()

    items
    |> Array.iter(fun t -> 
        match t with
        | Token.Undefined s -> 
            s.Split crlfcrlf
            |> Array.map Token.Undefined
            |> tokens.AddRange
        | _ -> tokens.Add t)

let indent (tokens:List<Token>) = 

    let items = tokens.ToArray()

    tokens.Clear()

    items
    |> Array.iter(fun t -> 
        match t with
        | Token.Undefined s -> 
            s.Length - s.TrimStart().Length
            |> Token.Newline
            |> tokens.Add
        | _ -> ()
        t |> tokens.Add)

let lex (src:string) = 

    let index = ref 0

    let tokens = new List<Token>()
    
    mask src tokens
    indent tokens

    tokens.ToArray()


let removeComment = Array.map(fun (i:string) -> 
    let index = i.IndexOf "//"
    if index >= 0 then
        i.Substring(0,index)
    else
        i)

type FSharpCompiler = {
output: string -> unit }

let go output file = 
    
    let compiler = {
        output = output }

    let lines = 
        file 
        |> try_read_string
        |> snd
        |> lex

    ()

