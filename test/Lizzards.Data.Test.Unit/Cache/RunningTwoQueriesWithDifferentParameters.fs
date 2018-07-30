module Lizzards.Data.Test.Unit.Cache.RunningTwoQueriesWithDifferentParameters

open Xunit
open Lizzards.Data.CQRS
open Lizzards.Data.Cache
open Microsoft.Extensions.Caching.Memory
open FsUnit
open Microsoft.Extensions.Caching.Distributed
open Microsoft.Extensions.Options

type QueryOptions(value:decimal) =
  member this.Value = value

type ComplexBasedQuery() =
    interface IQuery<decimal, QueryOptions> with
        member this.Execute param1 =
            param1.Value

let cacheOptions = new MemoryDistributedCacheOptions()
let cache = new MemoryDistributedCache(Options.Create(cacheOptions))

[<Fact>]
let ``Run two queries with different parameter and results should be different`` () =
    let query= new ComplexBasedQuery() :> IQuery<decimal, QueryOptions>
    let cachedQuery = new CachedQueryDecorator<decimal, QueryOptions>(query, cache) :> IQuery<decimal, QueryOptions>
    cachedQuery.Execute(new QueryOptions(1M)) |> ignore
    cachedQuery.Execute(new QueryOptions(2M)) |> ignore
    let first_result = cachedQuery.Execute(new QueryOptions(1M))
    let second_result = cachedQuery.Execute(new QueryOptions(2M))
    first_result |> should not' (equal second_result)

[<Fact>]
let ``Run two queries with same parameter and results should be different`` () =
    let query= new ComplexBasedQuery() :> IQuery<decimal, QueryOptions>
    let cachedQuery = new CachedQueryDecorator<decimal, QueryOptions>(query, cache) :> IQuery<decimal, QueryOptions>
    cachedQuery.Execute(new QueryOptions(1M)) |> ignore
    cachedQuery.Execute(new QueryOptions(2M)) |> ignore
    let first_result = cachedQuery.Execute(new QueryOptions(1M))
    let second_result = cachedQuery.Execute(new QueryOptions(1M))
    first_result |> should equal second_result
