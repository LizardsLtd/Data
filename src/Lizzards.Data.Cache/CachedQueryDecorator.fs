namespace Lizzards.Data.Cache

open System.Threading.Tasks
open Lizzards.Data.CQRS
open Microsoft.Extensions.Caching.Distributed

[<AbstractClass>]
type CacheDecoratorBase<'TReturnType>(cache: IDistributedCache) =
  member private this.Cache = new DistributedCache(cache)
  member this.GetOrCreate (key:string) (fallback: unit -> Task<'TReturnType>): Task<'TReturnType>  =
    let savedInCache = this.Cache.ContainsKey key
    let result: 'TReturnType =
        match savedInCache with
        | true -> this.Cache.Get<'TReturnType> key
        | false ->
            let fallbackResult: 'TReturnType = fallback() |> Async.AwaitTask |> Async.RunSynchronously
            this.Cache.Set key fallbackResult
            fallbackResult
    Task.FromResult result

type CachedQueryDecorator<'TPayload>(internalQuery: IQuery<'TPayload>, cache: IDistributedCache) =
  inherit CacheDecoratorBase<'TPayload>(cache)
  interface IQuery<'TPayload> with
    member this.Execute() =
      let fallback = fun () -> internalQuery.Execute()
      this.GetOrCreate (this.GetKey()) fallback
  member private this.GetKey(): string =
    typedefof<'TPayload>.Name

type CachedQueryDecorator<'TPayload, 'TParam1 when 'TParam1: equality>(internalQuery: IQuery<'TPayload, 'TParam1>, cache: IDistributedCache) =
  inherit CacheDecoratorBase<'TPayload>(cache)
  interface IQuery<'TPayload, 'TParam1> with
    member this.Execute(param1: 'TParam1) =
      let fallback = fun () -> internalQuery.Execute(param1)
      this.GetOrCreate (this.GetKey param1) fallback
  member private this.GetKey(param1:'TParam1) =
    typedefof<'TPayload>.Name + "::" + param1.GetHashCode().ToString()
