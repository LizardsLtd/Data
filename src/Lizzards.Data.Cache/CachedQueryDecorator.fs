namespace Lizzards.Data.Cache

open Lizards.Data.CQRS
open Microsoft.Extensions.Caching.Distributed
open System

type CachedQueryDecorator<'TPayload>(internalQuery: IQuery<'TPayload>, cache: IDistributedCache) =
  interface IQuery<'TPayload> with
    member this.Execute() =
      let key = this.getKey
      let fallbackFunction = fun () -> this.internalQuery.Execute()
      this.cache.Deserialize<'TPayload> key fallbackFunction

  member private this.internalQuery = internalQuery
  member private this.cache = new DistributedCache(cache)

  member this.getKey: string = typedefof<'TPayload>.Name;

type CachedQueryDecorator<'TPayload, 'TParam1>(internalQuery: IQuery<'TPayload, 'TParam1>, cache: IDistributedCache) =
  interface IQuery<'TPayload, 'TParam1> with
    member this.Execute(param1:'TParam1) =
      let key = this.getKey(param1)
      let fallbackFunction = fun () -> this.internalQuery.Execute(param1)
      this.cache.Deserialize<'TPayload> key fallbackFunction

  member private this.internalQuery = internalQuery
  member private this.cache = new DistributedCache(cache)
  member private this.getKey(param1:'TParam1) : string =
    typedefof<'TPayload>.Name + "::" + param1.ToString()

type CachedQueryDecorator<'TPayload, 'TParam1,'TParam2>(internalQuery: IQuery<'TPayload, 'TParam1, 'TParam2>, cache: IDistributedCache) =
  interface IQuery<'TPayload, 'TParam1, 'TParam2> with
    member this.Execute (param1:'TParam1, param2:'TParam2) =
      let key = this.getKey param1 param2
      let fallbackFunction = fun () -> this.internalQuery.Execute(param1, param2)
      this.cache.Deserialize<'TPayload> key fallbackFunction

  member private this.internalQuery = internalQuery
  member private this.cache = new DistributedCache(cache)
  member private this.getKey(param1:'TParam1) (param2: 'TParam2) : string =

    typedefof<'TPayload>.Name + "::" + param1.ToString() + "::" + param2.ToString()

type CachedQueryDecorator<'TPayload, 'TParam1, 'TParam2,'TParam3>(internalQuery: IQuery<'TPayload, 'TParam1, 'TParam2,'TParam3>, cache: IDistributedCache) =
  interface IQuery<'TPayload, 'TParam1, 'TParam2,'TParam3> with
    member this.Execute(param1:'TParam1, param2:'TParam2, param3: 'TParam3) =
      let key = this.getKey param1 param2 param3
      let fallbackFunction = fun () -> this.internalQuery.Execute(param1, param2, param3)
      this.cache.Deserialize<'TPayload> key fallbackFunction

  member private this.internalQuery = internalQuery
  member private this.cache = new DistributedCache(cache)
  member private this.getKey(param1:'TParam1) (param2: 'TParam2) (param3: 'TParam3) : string =
    typedefof<'TPayload>.Name + "::" + param1.ToString()+ "::" + param2.ToString() + "::" + param3.ToString()
