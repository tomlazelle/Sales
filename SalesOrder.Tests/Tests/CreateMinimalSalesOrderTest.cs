using System;
using AutoFixture;
using AutoMapper;
using SalesOrder.Domain.Configuration;
using SalesOrder.Domain.Handlers;
using SalesOrder.Domain.Messages;
using SalesOrder.Tests.Configuration;
using Shouldly;

namespace SalesOrder.Tests.Tests
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

            result.Id.ShouldBe(message.Id);
        }
    }
}