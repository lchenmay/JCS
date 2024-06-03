module BlazorLogics.Common

open System

open Util.Json
open Util.Text

open BizShared.OrmTypes
open BizShared.Types


Console.OutputEncoding <- System.Text.Encoding.Unicode
//let output (s:string) = Console.WriteLine s
let output (s:string) = System.Diagnostics.Debug.WriteLine s


let port = 12077
let server = "127.0.0.1"

let version = 7479



