module BizLogics.Init

open System
open System.IO
open System.Collections.Generic
open System.Collections.Concurrent
open System.Threading

open Util.Runtime
open Util.CollectionModDict
open Util.Bin
open Util.Json
open Util.Db
open Util.DbTx
open Util.Orm
open Util.Zmq

open UtilWebServer.Constants
open UtilWebServer.Db
open UtilWebServer.DbLogger
open UtilWebServer.Init
open UtilWebServer.FileSys

open Shared.OrmTypes
open Shared.OrmMor
open Shared.Types
open Shared.CustomMor

open BizLogics.Common

let init (runtime:Runtime) = 

    "Init ..."
    |> runtime.output

    conn <- runtime.host.conn

    if runtime.host.updateDatabase then
        updateDbStructure runtime conn

    Shared.OrmMor.init()





