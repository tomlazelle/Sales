using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Kernel;
using EventSource.Framework;
using EventSource.Framework.EventStores;
using Microsoft.Extensions.Options;
using NSubstitute;
using Raven.Client;
using Raven.Client.Converters;
using Raven.Client.Document;
using SalesOrder.Domain.Configuration;

namespace SalesOrder.Tests.Configuration
{
    public abstract class Subject<TClassUnderTest> : ISubjectBase
        where TClassUnderTest : class
    {
        protected IFixture _fixture;

        private TClassUnderTest _sut;

        protected TClassUnderTest Sut
        {
            get
            {
                return _sut ?? (_sut = new Lazy<TClassUnderTest>(() => _fixture.Create<TClassUnderTest>()).Value);
            }
        }


        public virtual void FixtureSetup(IFixture fixture)
        {
            _fixture = fixture;

            var configMgr = Substitute.For<IOptions<RavenDbSettings>>();
            configMgr.Value.Returns(new RavenDbSettings
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "SalesDb"
            })
            ;

            Register(new MockServiceProvider(_fixture));
            Register(configMgr);
        }

        public virtual void FixtureTearDown()
        {
        }

        protected void Register<TInterface>(TInterface concreteType)
        {
            _fixture.Register<TInterface>(() => concreteType);
        }

        protected T MockType<T>()
        {
            return _fixture.Create<T>();
        }

        public void RegisterDatabase()
        {
            var _store = new DocumentStore
            {
                Url = "http://localhost:8080/", // server URL
                DefaultDatabase = "EventSource",
                //                RunInMemory = true,
            };

            //            _store.Configuration.Storage.Voron.AllowOn32Bits = true;

            _store.Initialize();

            _store.Conventions.IdentityTypeConvertors = new List<ITypeConverter>
            {
                new GuidConverter()
            };

            //            IndexCreation.CreateIndexes(typeof(Zip).Assembly, _store);



            Register<IDocumentStore>(_store);
            Register<IEventStore>(new RavenDBEventStore(_store, new MockServiceProvider(_fixture)));
        }
    }


    public class MockServiceProvider : IServiceProvider
    {
        private readonly IFixture _fixture;

        public MockServiceProvider(IFixture fixture)
        {
            _fixture = fixture;
        }
        public object GetService(Type serviceType)
        {
            return new SpecimenContext(_fixture).Resolve(serviceType);
        }
    }
}