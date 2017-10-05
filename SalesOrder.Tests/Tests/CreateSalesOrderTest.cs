using AutoMapper;
using Ploeh.AutoFixture;
using Sales.Domain.Configuration;
using Sales.Domain.Handlers;
using Sales.Domain.Messages;
using Sales.Tests.Configuration;
using Should;

namespace Sales.Tests.Tests
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

            salesOrder.Id.ShouldEqual(createSalesOrder.Id);
            salesOrder.Items.Count.ShouldEqual(createSalesOrder.Items.Count);
        }
    }

    
}