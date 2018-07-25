namespace Lizzards.Data.Cache

open Microsoft.Extensions.Caching.Distributed
open System
open Newtonsoft.Json

type DistributedCache(cache: IDistributedCache) =
  member this.cache = cache

  member this.Serialize key item=
    let itemAsJson= JsonConvert.SerializeObject(item)
    this.cache.SetString(key, itemAsJson)

  member this.Deserialize<'T> (key: string)  (fallback: unit -> 'T) =
    let (|?) = defaultArg
    let serilaliziedItem = this.cache.GetString(key)
    let item =
      match serilaliziedItem with
      | x when System.String.IsNullOrWhiteSpace(x) ->
        let result = fallback()
        let serialized=  JsonConvert.SerializeObject(result)
        this.cache.SetString(key, serialized)
        result
      | _ ->
        let result =JsonConvert.DeserializeObject(serilaliziedItem)
        result :?> 'T
    item
