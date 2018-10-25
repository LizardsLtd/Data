module Lizzards.Data.Test.Unit.Cache.CachingDifferentTypes

open System
open Xunit
open Lizzards.Data.CQRS
open Lizzards.Data.Cache
open Microsoft.Extensions.Caching.Memory
open FsUnit
open Microsoft.Extensions.Caching.Distributed
open Microsoft.Extensions.Options
open System.ComponentModel.Design
open System.Threading.Tasks

type DecimalBasedQuery() =
    interface IQuery<decimal> with
        member this.Execute() =
            1M
type DecimalBasedAsyncQuery() =
    interface IQuery<decimal> with
        member this.Execute() =
            Task.FromResult 1M

let cacheOptions = new MemoryDistributedCacheOptions()
let cache = new MemoryDistributedCache(Options.Create(cacheOptions))

[<Fact>]
let ``IQuery can read decimal properly`` () =
    let query: IQuery<decimal> = new DecimalBasedQuery() :> IQuery<decimal>
    let cachedQuery = new CachedQueryDecorator<decimal>(query, cache) :> IQuery<decimal>
    let first_result = cachedQuery.Execute()
    let second_result = cachedQuery.Execute()
    first_result |> should equal second_result

[<Fact>]
let ``IQuery can read decimal properly`` () =
    let query: IQuery<decimal> = new DecimalBasedAsyncQuery() :> IQuery<decimal>
    let cachedQuery = new AsyncCachedQueryDecorator<decimal>(query, cache) :> IQuery<decimal>
    async {
        let! first_result = cachedQuery.Execute() |> Async.AwaitTask
        let! second_result = cachedQuery.Execute() |> Async.AwaitTask
        first_result |> should equal second_result
    }