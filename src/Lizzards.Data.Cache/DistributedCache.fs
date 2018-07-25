namespace Lizzards.Data.Cache

open Microsoft.Extensions.Caching.Distributed
open System
open Newtonsoft.Json

type DistributedCache(cache: IDistributedCache) =
  member this.Set key item=
    let itemAsJson= JsonConvert.SerializeObject(item)
    cache.SetString(key, itemAsJson)

  member this.ContainsKey key =
    let item = cache.GetString(key)
    let result =
      match item with
      | null | "" ->false
      | _ -> true
    result

  member this.Get<'T> (key: string)  (fallback: unit -> 'T) =
    let (|?) = defaultArg
    let serilaliziedItem = cache.GetString(key)
    let item =
      match serilaliziedItem with
      | x when System.String.IsNullOrWhiteSpace(x) ->
        let result = fallback()
        let serialized=  JsonConvert.SerializeObject(result)
        cache.SetString(key, serialized)
        result
      | _ ->
        let result =JsonConvert.DeserializeObject(serilaliziedItem)
        result :?> 'T
    item
