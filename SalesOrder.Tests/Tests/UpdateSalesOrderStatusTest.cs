using Ploeh.AutoFixture;
using Sales.Common;
using Sales.Domain.Handlers;
using Sales.Domain.Messages;
using Sales.Tests.Configuration;
using Should;

namespace Sales.Tests.Tests
{
    public class UpdateSalesOrderStatusTest:Subject<SalesOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);

            RegisterDatabase();
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

            result.Status.ShouldEqual(SalesOrderStatus.Pending);
        }
    }
}