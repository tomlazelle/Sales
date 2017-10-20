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

            For<ITypeActivator>().Use<TypeActivator>();
            For<IRavenDbConnection>().Use<RavenDbConnection>();            
            ForConcreteType<SalesOrderHandler>();
            For<IEventStore>().Use<RavenDBEventStore>();
            For<IEventPublisher>().Use<DummyPublisher>();
            For<IDocumentStore>().Singleton().Use(x => CreateNewStore(x.GetInstance<IRavenDbConnection>()));
        }

        private IDocumentStore CreateNewStore(IRavenDbConnection context)
        {
            var store = new DocumentStore
            {
                DefaultDatabase = context.DefaultDatabase,
                Url = context.Url
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