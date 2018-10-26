module Lizzards.Data.Test.Unit.Cache.IQueryFixture

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

type SingleQuery() =
    interface IQuery<string> with
        member this.Execute() =
            Task.FromResult(Guid.NewGuid().ToString())

type SingleQuery1() =
    interface IQuery<string,string> with
        member this.Execute(param1: string) =
            Task.FromResult(Guid.NewGuid().ToString())

let cacheOptions = new MemoryDistributedCacheOptions()
let cache = new MemoryDistributedCache(Options.Create(cacheOptions))

[<Fact>]
let ``Result should be GUID`` () =
  let query: IQuery<string> = new SingleQuery() :> IQuery<string>
  async {
    let! first_result = query.Execute() |> Async.AwaitTask
    let couldParse, parsedDate = Guid.TryParse(first_result)
    couldParse |> should equal true
  }

[<Fact>]
let ``Results from query should be difference every time`` () =
  let query: IQuery<string> = new SingleQuery() :> IQuery<string>
  async {
      let! first_result = query.Execute() |> Async.AwaitTask
      let! second_result = query.Execute() |> Async.AwaitTask
      first_result |> should not' (equal second_result)
  }

[<Fact>]
let ``Results from cached query should be the same every time`` () =
  let query = new SingleQuery()
  let cachedQuery = new CachedQueryDecorator<string>(query, cache) :> IQuery<string>
  async {
      let! first_result = cachedQuery.Execute() |> Async.AwaitTask
      let! second_result = cachedQuery.Execute() |> Async.AwaitTask
      first_result |> should equal second_result
  }

[<Theory>]
[<InlineData("one", "one", true)>]
[<InlineData("one", "two", false)>]
let ``Results from cached query with 1 parameter should be`` (key1, key2, expectedResult) =
  let query = new SingleQuery1()
  let cachedQuery = new CachedQueryDecorator<string, string>(query, cache) :> IQuery<string, string>
  async {
      let! first_result = cachedQuery.Execute(key1) |> Async.AwaitTask
      let! second_result = cachedQuery.Execute(key2) |> Async.AwaitTask
      let actualResult = first_result.Equals(second_result)
      actualResult |> should equal expectedResult
  }
