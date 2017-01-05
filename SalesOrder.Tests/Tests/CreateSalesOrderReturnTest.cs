using System.Linq;
using Ploeh.AutoFixture;
using Sales.Common;
using Sales.Domain.Handlers;
using Sales.Domain.Messages;
using Sales.Tests.Configuration;
using Should;

namespace Sales.Tests.Tests
{
    public class CreateSalesOrderReturnTest:Subject<SalesOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);
            RegisterDatabase();
        }

        public void can_add_a_claim(){
            var createSalesOrder = _fixture.Create<CreateSalesOrderMessage>();
            var salesOrder = Sut.Handle(createSalesOrder);
            var createReturnMessage = _fixture.Build<CreateReturnMessage>()
                .With(x => x.Id, salesOrder.Id)
                .With(x => x.Sku, salesOrder.Items.First().Sku)
                .With(x => x.Amount, salesOrder.Items.First().RetailPrice*salesOrder.Items.First().Quantity)
                .With(x => x.Quantity, salesOrder.Items.First().Quantity)
                .With(x => x.Action, ReturnAction.Reprint)
                .With(x => x.Reason, ReturnReasons.CustomerUnhappy)
                .With(x => x.Note, "ImaTest")
                .Create();

            var salesOrderWithReturn = Sut.Handle(createReturnMessage);

            salesOrderWithReturn.Returns.Count.ShouldEqual(1);
            salesOrderWithReturn.Returns.First().Sku.ShouldEqual(salesOrder.Items.First().Sku);
            salesOrderWithReturn.Returns.First().ReturnId.ShouldNotBeNull();
        }
    }
}