namespace Lizzards.Data.Cache

open Microsoft.Extensions.Caching.Distributed
open System
open Newtonsoft.Json

type DistributedCache(cache: IDistributedCache) =
  member this.ContainsKey key =
    let item = cache.GetString(key)
    let result =
      match item with
      | null | "" ->false
      | _ -> true
    result

  member this.Get<'T> (key:string) : 'T =
    let serilaliziedItem = cache.GetString(key)
    JsonConvert.DeserializeObject<'T>(serilaliziedItem)

  member this.Set key item =
    let itemAsJson = JsonConvert.SerializeObject(item)
    cache.SetString(key, itemAsJson)