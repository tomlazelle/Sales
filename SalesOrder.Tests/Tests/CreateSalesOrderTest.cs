using AutoFixture;
using AutoMapper;
using SalesOrder.Domain.Configuration;
using SalesOrder.Domain.Handlers;
using SalesOrder.Domain.Messages;
using SalesOrder.Tests.Configuration;
using Shouldly;

namespace SalesOrder.Tests.Tests
{
    public class CreateSalesOrderTest:Subject<SalesOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);

            RegisterDatabase();
            Register(new MapperConfiguration(x => x.AddProfile(new SalesOrderHandlerMapProfile())).CreateMapper());
        }

        public void can_create_a_sales_order()
        {
            var createSalesOrder = _fixture.Create<CreateSalesOrderMessage>();

            var salesOrder = Sut.Handle(createSalesOrder);

            salesOrder.Id.ShouldBe(createSalesOrder.Id);
            salesOrder.Items.Count.ShouldBe(createSalesOrder.Items.Count);
        }
    }

    
}