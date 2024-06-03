module WebLogics.DataService

open System
open System.Collections.Generic
open System.Collections.Concurrent

open Util.Cat
open Util.Bin
open Util.Zmq
open Util.IA

open BizShared.OrmTypes
open BizShared.Types
open BizShared.CustomMor

open WebLogics.Common


let client = {
    wsClient = "ws://" + server + ":" + port.ToString() |> create__WsClient 5000
    Fact__bin = FactBroadcast__bin
    bin__Fact = bin__FactBroadcast
    incomingFacts = new List<IncomingFacts<FactBroadcast>>() }


let init() = 
    connect client
    ()