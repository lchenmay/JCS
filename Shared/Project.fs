module Shared.Project

open System
open System.Threading
open System.Text
open System.Collections.Generic
open System.Collections.Concurrent

open Util.Text
open Util.Perf
open Util.Crypto
open Util.CollectionModDict
open Util.Db
open Util.DbTx
open Util.Orm
open Util.Zmq

open Shared.OrmTypes
open Shared.Types
open Shared.OrmMor
open Shared.CustomMor

let project__ProjectComplex project = 
    {   hostconfigs = createModDictStr 4    
        tables = createModDictStr 4
        comps = createModDictInt64 4
        templates = createModDictInt64 4
        pages = createModDictInt64 4
        apis = createModDictInt64 4
        project = project }

let comp__CompComplex comp = 
    {
        states = createModDictStr 4
        props = createModDictStr 4
        comp = comp }

let page__CompComplex page = 
    {
        states = createModDictStr 4
        props = createModDictStr 4
        page = page }

let api__ApiComplex api = 
    {
        reqs = createModDictStr 4
        reps = createModDictStr 4
        api = api }
