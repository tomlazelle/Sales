using System.Linq;
using Ploeh.AutoFixture;
using Sales.Common;
using Sales.Domain.Handlers;
using Sales.Domain.Messages;
using Sales.Tests.Configuration;
using Should;

namespace Sales.Tests.Tests
{
    public class UpdateReturnStatusTest:Subject<SalesOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);
            RegisterDatabase();
        }

        public void can_update_claim_status(){
            var createSalesOrder = _fixture.Create<CreateSalesOrderMessage>();
            var salesOrder = Sut.Handle(createSalesOrder);
            var createReturnMessage = _fixture.Build<CreateReturnMessage>()
                .With(x => x.Id, salesOrder.Id)
                .With(x => x.Sku, salesOrder.Items.First().Sku)
                .With(x => x.Amount, salesOrder.Items.First().RetailPrice * salesOrder.Items.First().Quantity)
                .With(x => x.Quantity, salesOrder.Items.First().Quantity)
                .With(x => x.Action, ReturnAction.Reprint)
                .With(x => x.Reason, ReturnReasons.CustomerUnhappy)
                .With(x => x.Note, "ImaTest")
                .Create();

            var salesOrderWithReturn = Sut.Handle(createReturnMessage);

            var claim = salesOrderWithReturn.Returns.First();

            var updatedSalesOrderClaim = Sut.Handle(new UpdateReturnStatusMessage
            {
                Id = salesOrderWithReturn.Id,
                ReturnId = claim.ReturnId,
                Status = ReturnStatus.Completed
            });

            updatedSalesOrderClaim.Returns.First().Status.ShouldEqual(ReturnStatus.Completed);
        }
    }
}