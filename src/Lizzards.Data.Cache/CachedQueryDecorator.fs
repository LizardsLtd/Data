namespace Lizzards.Data.Cache

open Lizards.Data.CQRS
open Microsoft.Extensions.Caching.Distributed
open System

[<AbstractClass>]
type CacheDecoratorBase<'TReturnType>(cache: IDistributedCache) =
  member private this.Cache = new DistributedCache(cache)
  member this.GetOrCreate (key:string) fallback =
    let savedInCache = this.Cache.ContainsKey key
    let result: 'TReturnType =
        match savedInCache with
        | true -> this.Cache.Get<'TReturnType> key
        | false ->
            let result = fallback()
            this.Cache.Set key result
            result
    result

type CachedQueryDecorator<'TPayload>(internalQuery: IQuery<'TPayload>, cache: IDistributedCache) =
  inherit CacheDecoratorBase<'TPayload>(cache)
  interface IQuery<'TPayload> with
    member this.Execute() =
      let fallback = fun () -> internalQuery.Execute()
      this.GetOrCreate (this.GetKey()) fallback
  member private this.GetKey(): string =
    typedefof<'TPayload>.Name

type CachedQueryDecorator<'TPayload, 'TParam1>(internalQuery: IQuery<'TPayload, 'TParam1>, cache: IDistributedCache) =
  inherit CacheDecoratorBase<'TPayload>(cache)
  interface IQuery<'TPayload, 'TParam1> with
    member this.Execute(param1: 'TParam1) =
      let fallback = fun () -> internalQuery.Execute(param1)
      this.GetOrCreate (this.GetKey param1) fallback
  member private this.GetKey(param1:'TParam1) =
    typedefof<'TPayload>.Name + "::" + param1.ToString()

type CachedQueryDecorator<'TPayload, 'TParam1,'TParam2>(internalQuery: IQuery<'TPayload, 'TParam1, 'TParam2>, cache: IDistributedCache) =
  inherit CacheDecoratorBase<'TPayload>(cache)
  interface IQuery<'TPayload, 'TParam1, 'TParam2> with
    member this.Execute (param1:'TParam1, param2:'TParam2) =
      let fallback = fun () -> internalQuery.Execute(param1, param2)
      this.GetOrCreate (this.GetKey param1 param2) fallback
  member private this.GetKey(param1:'TParam1) (param2: 'TParam2) : string =
    typedefof<'TPayload>.Name + "::" + param1.ToString() + "::" + param2.ToString()

type CachedQueryDecorator<'TPayload, 'TParam1, 'TParam2,'TParam3>(internalQuery: IQuery<'TPayload, 'TParam1, 'TParam2,'TParam3>, cache: IDistributedCache) =
  inherit CacheDecoratorBase<'TPayload>(cache)
  interface IQuery<'TPayload, 'TParam1, 'TParam2,'TParam3> with
    member this.Execute(param1:'TParam1, param2:'TParam2, param3: 'TParam3) =
      let fallback = fun () -> internalQuery.Execute(param1, param2, param3)
      this.GetOrCreate (this.GetKey param1 param2 param3) fallback
  member private this.GetKey(param1:'TParam1) (param2: 'TParam2) (param3: 'TParam3) : string =
    typedefof<'TPayload>.Name + "::" + param1.ToString()+ "::" + param2.ToString() + "::" + param3.ToString()