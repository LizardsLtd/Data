module Lizzards.Data.Test.Unit.Cache.Queries

open System
open Xunit
open Lizards.Data.CQRS

type SimpleQuery =
    member this.HasBeenCalled() =
        false
    interface IQuery<string> with
        member this.Execute() =
            "this is test"

[<Fact>]
let ``My test`` () =
  Assert.True(true)