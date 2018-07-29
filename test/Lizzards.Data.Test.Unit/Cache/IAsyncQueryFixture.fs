module Lizzards.Data.Test.Unit.Cache.IAsyncQueryFixture

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

type SingleAsyncQuery() =
    interface IAsyncQuery<string> with
        member this.Execute() =
            Task.FromResult(Guid.NewGuid().ToString())

type SingleAsyncQuery1() =
    interface IAsyncQuery<string,string> with
        member this.Execute(param1: string) =
            Task.FromResult(Guid.NewGuid().ToString())

type SingleAsyncQuery2() =
    interface IAsyncQuery<string,string,string> with
        member this.Execute(param1: string,param2:string) =
            Task.FromResult(Guid.NewGuid().ToString())

type SingleAsyncQuery3() =
    interface IAsyncQuery<string,string,string,string> with
        member this.Execute(param1: string,param2:string, param3:string) =
            Task.FromResult(Guid.NewGuid().ToString())

let cacheOptions = new MemoryDistributedCacheOptions()
let cache = new MemoryDistributedCache(Options.Create(cacheOptions))

[<Fact>]
let ``Result should be GUID`` () =
  let query: IAsyncQuery<string> = new SingleAsyncQuery() :> IAsyncQuery<string>
  async {
    let! first_result = query.Execute() |> Async.AwaitTask
    let couldParse, parsedDate = Guid.TryParse(first_result)
    couldParse |> should equal true
  }

[<Fact>]
let ``Results from query should be difference every time`` () =
  let query: IAsyncQuery<string> = new SingleAsyncQuery() :> IAsyncQuery<string>
  async {
      let! first_result = query.Execute() |> Async.AwaitTask
      let! second_result = query.Execute() |> Async.AwaitTask
      first_result |> should not' (equal second_result)
  }

[<Fact>]
let ``Results from cached query should be the same every time`` () =
  let query = new SingleAsyncQuery()
  let cachedQuery = new AsyncCachedQueryDecorator<string>(query, cache) :> IAsyncQuery<string>
  async {
      let! first_result = cachedQuery.Execute() |> Async.AwaitTask
      let! second_result = cachedQuery.Execute() |> Async.AwaitTask
      first_result |> should equal second_result
  }

[<Theory>]
[<InlineData("one", "one", true)>]
[<InlineData("one", "two", false)>]
let ``Results from cached query with 1 parameter should be`` (key1, key2, expectedResult) =
  let query = new SingleAsyncQuery1()
  let cachedQuery = new AsyncCachedQueryDecorator<string, string>(query, cache) :> IAsyncQuery<string, string>
  async {
      let! first_result = cachedQuery.Execute(key1) |> Async.AwaitTask
      let! second_result = cachedQuery.Execute(key2) |> Async.AwaitTask
      let actualResult = first_result.Equals(second_result)
      actualResult |> should equal expectedResult
  }

[<Theory>]
[<InlineData("one", "one", true)>]
[<InlineData("one", "two", false)>]
let ``Results from cached query with 2 parameter should be`` (key1, key2, expectedResult) =
  let query = new SingleAsyncQuery2()
  let cachedQuery = new AsyncCachedQueryDecorator<string, string, string>(query, cache) :> IAsyncQuery<string, string, string>
  async {
      let! first_result = cachedQuery.Execute(key1, key1) |> Async.AwaitTask
      let! second_result = cachedQuery.Execute(key2, key2) |> Async.AwaitTask
      let actualResult = first_result.Equals(second_result)
      actualResult |> should equal expectedResult
  }

[<Theory>]
[<InlineData("one", "one", true)>]
[<InlineData("one", "two", false)>]
let ``Results from cached query with 3 parameter should be`` (key1, key2, expectedResult) =
  let query = new SingleAsyncQuery3()
  let cachedQuery = new AsyncCachedQueryDecorator<string, string, string, string>(query, cache) :> IAsyncQuery<string, string, string, string>
  async {
      let! first_result = cachedQuery.Execute(key1,key1,key1) |> Async.AwaitTask
      let! second_result = cachedQuery.Execute(key2,key2,key2) |> Async.AwaitTask
      let actualResult = first_result.Equals(second_result)
      actualResult |> should equal expectedResult
  }