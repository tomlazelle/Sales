using AutoFixture;
using AutoMapper;
using SalesOrder.Common;
using SalesOrder.Domain.Configuration;
using SalesOrder.Domain.Handlers;
using SalesOrder.Domain.Messages;
using SalesOrder.Tests.Configuration;
using Shouldly;

namespace SalesOrder.Tests.Tests
{
    public class UpdateSalesOrderStatusTest:Subject<SalesOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);

            RegisterDatabase();
            Register(new MapperConfiguration(x => x.AddProfile(new SalesOrderHandlerMapProfile())).CreateMapper());
        }

        public void can_update_sales_order_status(){
            var createSalesOrder = _fixture.Create<CreateSalesOrderMessage>();
            createSalesOrder.Status = SalesOrderStatus.Open;

            var salesOrder = Sut.Handle(createSalesOrder);

            var result = Sut.Handle(new UpdateSalesOrderStatusMessage
            {
                Id = salesOrder.Id,
                Status = SalesOrderStatus.Pending
            });

            result.Status.ShouldBe(SalesOrderStatus.Pending);
        }
    }
}