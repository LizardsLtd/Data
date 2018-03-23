module Lizards.Tests.Data.EventBusCanSelectHandler

    open System
    open Lizards.Data.Events
    open Xunit
    open FakeItEasy

    type TestEventAlpha(id: Guid) =
        interface IEvent with
            member this.EventId = id

    type TestEventBeta(id: Guid) =
        interface IEvent with
            member this.EventId = id

    let eventBus  = new EventBus()

    [<Fact>]
    let ``EventBus can execute the Event`` () =
        let event = new TestEventAlpha(Guid.NewGuid())
        eventBus.Publish(event)