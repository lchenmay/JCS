﻿module WebLogics.Common

open System

open Util.Json
open Util.Text

open BizShared.OrmTypes
open BizShared.Types


Console.OutputEncoding <- System.Text.Encoding.Unicode
//let output (s:string) = Console.WriteLine s
let output (s:string) = System.Diagnostics.Debug.WriteLine s

let version = 7483



