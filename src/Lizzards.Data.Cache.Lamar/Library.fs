namespace Lizzards.Data.Cache.Lamar

open Lamar
open Lamar.Scanning.Conventions
open Lamar.Scanning
open Lizards.Data.CQRS
open Microsoft.Extensions.DependencyInjection

type QueryConvention() =
    interface IRegistrationConvention with
        member this.ScanTypes (types:TypeSet) (services:ServiceRegistry): unit =
            let baseType = typedefof<IQuery<_>>
            types.FindTypes TypeClassification.Concretes
                |> Seq.find (fun t -> baseType.IsAssignableFrom t)
                |> Seq.iter (fun item -> services.AddTransient(
            unit