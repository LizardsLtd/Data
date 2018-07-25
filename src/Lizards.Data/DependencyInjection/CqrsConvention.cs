﻿namespace Lizards.Data.DependencyInjection
{
    using Lamar;
    using Lamar.Scanning;
    using Lamar.Scanning.Conventions;
    using Lizards.Data.CQRS;
    using Lizards.Data.CQRS.DataAccess;
    using Lizards.Data.Events;

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