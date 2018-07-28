namespace Lizzards.Data.DependencyInjection
{
    using Lamar;
    using Lamar.Scanning;
    using Lamar.Scanning.Conventions;
    using Lizzards.Data.CQRS;
    using Lizzards.Data.CQRS.DataAccess;
    using Lizzards.Data.Events;

    public sealed class CqrsConvention : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, ServiceRegistry services)
        {
            new LoadAllChildTypesConvertion<IsQuery>().ScanTypes(types, services);
            new LoadAllChildTypesConvertion<IDataContext>().ScanTypes(types, services);
            new LoadAllChildTypesConvertion<IDataContextInitialiser>().ScanTypes(types, services);
            new LoadAllChildTypesConvertion<ICommandBus>().ScanTypes(types, services);
            new LoadAllChildTypesConvertion<IEventBus>().ScanTypes(types, services);
        }
    }
}