using System;
using AutoMapper;
using Ploeh.AutoFixture;
using Sales.Domain.Configuration;
using Sales.Domain.Handlers;
using Sales.Domain.Messages;
using Sales.Tests.Configuration;
using Should;

namespace Sales.Tests.Tests
{
    public class CreateMinimalSalesOrderTest:Subject<SalesOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);
            Register(new MapperConfiguration(x=>x.AddProfile(new SalesOrderHandlerMapProfile())).CreateMapper());
        }

        public void can_create_a_return_with_minimal_information(){
            var message = new CreateSalesOrderMessage
            {
                Id = Guid.NewGuid(),
                BillingAddress = _fixture.Create<AddressMessage>()
            };


            var result = Sut.Handle(message);

            result.Id.ShouldEqual(message.Id);
        }
    }
}