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

type SingleQuery() =
    interface IQuery<string> with
        member this.Execute() =
            Guid.NewGuid().ToString()

type SingleQuery1() =
    interface IQuery<string,string> with
        member this.Execute(param1: string) =
            Guid.NewGuid().ToString()

type SingleQuery2() =
    interface IQuery<string,string,string> with
        member this.Execute(param1: string,param2:string) =
            Guid.NewGuid().ToString()

type SingleQuery3() =
    interface IQuery<string,string,string,string> with
        member this.Execute(param1: string,param2:string, param3:string) =
            Guid.NewGuid().ToString()

let cacheOptions = new MemoryDistributedCacheOptions()
let cache = new MemoryDistributedCache(Options.Create(cacheOptions))

[<Fact>]
let ``Results from query should be difference every time`` () =
  let query: IQuery<string> = new SingleQuery() :> IQuery<string>
  let first_result = query.Execute()
  let second_result = query.Execute()
  first_result |> should not' (equal second_result)

[<Fact>]
let ``Results from cached query should be the same every time`` () =
  let query = new SingleQuery()
  let cachedQuery = new CachedQueryDecorator<string>(query, cache) :> IQuery<string>
  let first_result = cachedQuery.Execute()
  let second_result = cachedQuery.Execute()
  first_result |> should equal second_result

[<Theory>]
[<InlineData("one", "one", true)>]
[<InlineData("one", "two", false)>]
let ``Results from cached query with 1 parameter should be`` (key1, key2, expectedResult) =
  let query = new SingleQuery1()
  let cachedQuery = new CachedQueryDecorator<string, string>(query, cache) :> IQuery<string, string>
  let first_result = cachedQuery.Execute(key1)
  let second_result = cachedQuery.Execute(key2)
  let actualResult = first_result.Equals(second_result)
  actualResult |> should equal expectedResult

[<Theory>]
[<InlineData("one", "one", true)>]
[<InlineData("one", "two", false)>]
let ``Results from cached query with 2 parameter should be`` (key1, key2, expectedResult) =
  let query = new SingleQuery2()
  let cachedQuery = new CachedQueryDecorator<string, string, string>(query, cache) :> IQuery<string, string, string>
  let first_result = cachedQuery.Execute(key1, key1)
  let second_result = cachedQuery.Execute(key2, key2)
  let actualResult = first_result.Equals(second_result)
  actualResult |> should equal expectedResult

[<Theory>]
[<InlineData("one", "one", true)>]
[<InlineData("one", "two", false)>]
let ``Results from cached query with 3 parameter should be`` (key1, key2, expectedResult) =
  let query = new SingleQuery3()
  let cachedQuery = new CachedQueryDecorator<string, string, string, string>(query, cache) :> IQuery<string, string, string, string>
  let first_result = cachedQuery.Execute(key1,key1,key1)
  let second_result = cachedQuery.Execute(key2,key2,key2)
  let actualResult = first_result.Equals(second_result)
  actualResult |> should equal expectedResult