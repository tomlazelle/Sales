using System.Linq;
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
    public class AddReturnNoteTest:Subject<SalesOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);
            RegisterDatabase();
            Register(new MapperConfiguration(x => x.AddProfile(new SalesOrderHandlerMapProfile())).CreateMapper());
        }

        public void can_add_a_return_note(){
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

            var resultSalesOrder = Sut.Handle(new AddReturnNoteMessage
            {
                Id = salesOrder.Id,
                Note = "this is a second note",
                ReturnId = salesOrderWithReturn.Returns.First().ReturnId
            });

            resultSalesOrder.Returns.First().Notes.Count.ShouldBe(2);
        }
    }
}