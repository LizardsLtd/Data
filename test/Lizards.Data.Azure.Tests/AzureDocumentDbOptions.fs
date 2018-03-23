module Lizards.Data.Azure.Tests.AzureDocumentDbOptionsTests

    open System
    open System.Linq
    open Xunit
    open FsUnit.Xunit
    open Microsoft.Extensions.Configuration
    open Lizards.Data.Domain
    open Lizards.Data.Azure

    type User =
        interface IAggregateRoot with
                    member this.Id = Guid.NewGuid()
    type Login =
        interface IAggregateRoot with
            member this.Id = Guid.NewGuid()
    type Audit =
        interface IAggregateRoot with
            member this.Id = Guid.NewGuid()
    type SomethingElse =
        interface IAggregateRoot with
            member this.Id = Guid.NewGuid()

    let dict = dict ["Databases:0:Name", "FirstDB";
        "Databases:0:Name", "FirstDB";
        "Databases:0:Type", "User";
        "Databases:0:Collection", "Users";
        "Databases:1:Name", "FirstDB";
        "Databases:1:Type", "Login";
        "Databases:1:Collection", "Logins";
        "Databases:2:Name", "SecondDB";
        "Databases:2:Type", "Audit";
        "Databases:2:Collection", "Audits";
        "Databases:3:Name", "SecondDB";
        "Databases:3:Type", "SomethingElse";
        "Databases:3:Collection", "Collections";
        "ConnectionString:AccountEndpoint", "AccountEndpoint";
        "ConnectionString:AccountKey", "AccountKey";]
    let configuration = ConfigurationBuilder().AddInMemoryCollection(dict).Build()
    let LoadOptins() =
        let options = AzureDocumentDbOptions()
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        options

    [<Fact>]
    let ``Can load databases properly`` () =
        let options = AzureDocumentDbOptions()
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        options.GetDatabasesCollections().Count() |> should equal 4

    [<Theory>]
    [<InlineData(0, "FirstDB")>]
    [<InlineData(1, "FirstDB")>]
    [<InlineData(2, "SecondDB")>]
    [<InlineData(3, "SecondDB")>]
    let ``Database name is correct`` (dbIndex: int, expected: string) =
        let options = AzureDocumentDbOptions();
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        let database = options.GetDatabasesCollections().ElementAt(dbIndex)
        database.DatabaseId |> should equal expected

    [<Theory>]
    [<InlineData(0, "User")>]
    [<InlineData(1, "Login")>]
    [<InlineData(2, "Audit")>]
    [<InlineData(3, "SomethingElse")>]
    let ``Type is correct`` (dbIndex: int, expected: string) =
        let options = AzureDocumentDbOptions();
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        let database = options.GetDatabasesCollections().ElementAt(dbIndex)
        database.AggregateRoot |> should equal expected

    [<Theory>]
    [<InlineData(0, "Users")>]
    [<InlineData(1, "Logins")>]
    [<InlineData(2, "Audits")>]
    [<InlineData(3, "Collections")>]
    let ``Collection is correct`` (dbIndex: int, expected: string) =
        let options = AzureDocumentDbOptions();
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        let database = options.GetDatabasesCollections().ElementAt(dbIndex)
        database.Collection |> should equal expected

    [<Fact>]
    let ``Can GetDatabaseConfig for User`` () =
        let options = AzureDocumentDbOptions();
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        let struct (database,  collection) = options.GetDatabaseConfig<User>()
        database |> should equal "FirstDB"
        collection |> should equal "Users"

    [<Fact>]
    let ``Can GetDatabaseConfig for Login`` () =
        let options = AzureDocumentDbOptions();
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        let struct (database,  collection) = options.GetDatabaseConfig<Login>()
        database |> should equal "FirstDB"
        collection |> should equal "Logins"

    [<Fact>]
    let ``Can GetDatabaseConfig for Audit`` () =
        let options = AzureDocumentDbOptions();
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        let struct (database,  collection) = options.GetDatabaseConfig<Audit>()
        database |> should equal "SecondDB"
        collection |> should equal "Audits"

    [<Fact>]
    let ``Can GetDatabaseConfig for SomethingElse`` () =
        let options = AzureDocumentDbOptions();
        AzureDocumentDbOptions.FromConfiguration(configuration, options)
        let struct (database,  collection) = options.GetDatabaseConfig<SomethingElse>()
        database |> should equal "SecondDB"
        collection |> should equal "Collections"