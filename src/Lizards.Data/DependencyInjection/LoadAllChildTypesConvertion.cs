namespace Lizards.Data.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Lamar;
    using Lamar.Scanning;
    using Lamar.Scanning.Conventions;
    using Microsoft.Extensions.DependencyInjection;

    public sealed class LoadAllChildTypesConvertion<TLoadAllChildrenOfThis> : IRegistrationConvention
    {
        private Func<IServiceCollection, Type, Type, IServiceCollection> AddToService;

        public LoadAllChildTypesConvertion()
        {
            this.AddToService = ServiceCollectionServiceExtensions.AddTransient;
        }

        public void ScanTypes(TypeSet types, ServiceRegistry services)
        {
            var filtredTypes = this.GetFiltredByInterfaceTypes(types);

            foreach (var type in filtredTypes)
            {
                foreach (var @interface in type.GetInterfaces())
                {
                    this.AddToService(services, @interface, type);
                }
            }
        }

        private IEnumerable<Type> GetFiltredByInterfaceTypes(TypeSet types)
        {
            var baseType = typeof(TLoadAllChildrenOfThis);
            return types
                .FindTypes(TypeClassification.Concretes)
                .Where(x => baseType.IsAssignableFrom(x));
        }
    }
}