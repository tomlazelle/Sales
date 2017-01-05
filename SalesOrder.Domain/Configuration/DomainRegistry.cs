using System.Collections.Generic;
using EventSource.Framework;
using Raven.Client;
using Raven.Client.Converters;
using Raven.Client.Document;
using Sales.Common;
using Sales.Domain.EventStores;
using Sales.Domain.Handlers;
using StructureMap;

namespace Sales.Domain.Configuration
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            For<IDocumentStore>().Singleton().Use(x => CreateNewStore(x));
            ForConcreteType<SalesOrderHandler>();
            For<IEventStore>().Use<RavenDBEventStore>();


        }

        private IDocumentStore CreateNewStore(IContext context)
        {
            var store = new DocumentStore
            {
                DefaultDatabase = "Sales",
                Url = context.GetInstance<IConfigMgr>().Get<string>("SalesDb")
            }.Initialize();



            store.Conventions.IdentityTypeConvertors = new List<ITypeConverter>
            {
                new GuidConverter()
            };

            //            IndexCreation.CreateIndexes(typeof(Zip).Assembly, _store);
            return store;
        }
    }
}