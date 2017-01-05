using System;
using System.Linq;
using SystemNotifications;
using EventSource.Framework;
using Sales.Common;
using Sales.Domain.Aggregates;
using Sales.Domain.Events;
using Sales.Domain.Messages;

namespace Sales.Domain.Handlers
{
    public class SalesOrderHandler : IMessageHandler<CreateSalesOrderMessage, SalesOrder>
    {
        private IEventPublisher _eventPublisher;
        private IEventStore _eventStore;

        public SalesOrderHandler(
            IEventStore eventStore,
            IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        public SalesOrder Handle(CreateSalesOrderMessage message)
        {
            var salesOrderCreatedEvent = new SalesOrderCreatedEvent(
                message.Id,
                message.AccountId,
                new Address(
                    message.ShippingAddress.Address1,
                    message.ShippingAddress.Address2,
                    message.ShippingAddress.Address3,
                    message.ShippingAddress.City,
                    message.ShippingAddress.State,
                    message.ShippingAddress.PostalCode),
                new Address(
                    message.BillingAddress.Address1,
                    message.BillingAddress.Address2,
                    message.BillingAddress.Address3,
                    message.BillingAddress.City,
                    message.BillingAddress.State,
                    message.BillingAddress.PostalCode),
                new Person(
                    message.Customer.FirstName,
                    message.Customer.LastName,
                    message.Customer.Phone,
                    message.Customer.Email),
                new Payment(
                    message.Payment.PaymentType,
                    message.Payment.Amount,
                    message.Payment.PaymentDate,
                    message.Payment.PayeeName),
                message.OrderDate,
                message.SubTotal,
                message.Tax,
                message.Total,
                message.DollarsOff,
                message.DiscountPercent,
               message.Status,
               message.OrderType,
               message.RefNo,
                message.Items.Select(x =>
                    new CreateOrderItemEvent(
                        x.Sku,
                        x.Quantity,
                        x.WholeSalePrice,
                        x.RetailPrice,
                        x.DollarsOff,
                        x.DiscountPercent,
                        x.Details)).ToList());

            var events = _eventStore.AddEvent<SalesOrderEvents>(message.Id, salesOrderCreatedEvent);

            _eventPublisher.Publish(new SalesOrderCreated());

            return new SalesOrder(message.Id, events);
        }

        public SalesOrder Handle(UpdateSalesOrderStatusMessage updateSalesOrderStatusMessage)
        {
            var events = _eventStore.AddEvent<SalesOrderEvents>(updateSalesOrderStatusMessage.Id,
                new UpdateSalesOrderStatusEvent(updateSalesOrderStatusMessage.Id, updateSalesOrderStatusMessage.Status));

            _eventPublisher.Publish(new SalesOrderStatusChanged());

            return new SalesOrder(updateSalesOrderStatusMessage.Id, events);
        }

        public SalesOrder Handle(CreateReturnMessage createReturnMessage)
        {
            var salesOrder = new SalesOrder(createReturnMessage.Id, _eventStore.Get<SalesOrderEvents>(createReturnMessage.Id));

            var returnCnt = salesOrder.Returns.Count + 1;

            var events = _eventStore.AddEvent<SalesOrderEvents>(createReturnMessage.Id,
                new CreateReturnEvent(createReturnMessage.Id,
                createReturnMessage.Amount,
                createReturnMessage.Quantity,
                createReturnMessage.Sku,
                createReturnMessage.Reason,
                createReturnMessage.Action,
                createReturnMessage.Note,
                DateTime.Now,
                DateTime.Now.ToString("yyMMdd") + returnCnt.ToString().PadLeft(4, char.Parse("0")),
                ReturnStatus.Pending));

            _eventPublisher.Publish(new CustomerReturnCreated());

            return new SalesOrder(createReturnMessage.Id, events);
        }

        public SalesOrder Handle(UpdateReturnStatusMessage updateSalesOrderStatusMessage)
        {
            var events = _eventStore.AddEvent<SalesOrderEvents>(updateSalesOrderStatusMessage.Id,
                new UpdateReturnStatusEvent(updateSalesOrderStatusMessage.Id, updateSalesOrderStatusMessage.Status,
                updateSalesOrderStatusMessage.ReturnId));

            _eventPublisher.Publish(new ReturnStatusChanged());

            return new SalesOrder(updateSalesOrderStatusMessage.Id, events);
        }

        public SalesOrder Handle(AddReturnNoteMessage addReturnNoteMessage)
        {
            var events = _eventStore.AddEvent<SalesOrderEvents>(addReturnNoteMessage.Id,
               new AddReturnNoteEvent(addReturnNoteMessage.Id, addReturnNoteMessage.Note, addReturnNoteMessage.ReturnId));

            
            return new SalesOrder(addReturnNoteMessage.Id, events);
        }
    }
}