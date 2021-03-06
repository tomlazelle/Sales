﻿using System;
using System.Linq;
using SystemNotifications;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private IEventStore _eventStore;

        public SalesOrderHandler(
            IEventStore eventStore,
            IEventPublisher eventPublisher,
            IMapper mapper)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
            _mapper = mapper;
        }

        public SalesOrder Handle(CreateSalesOrderMessage message)
        {
            var salesOrderCreatedEvent = _mapper.Map<SalesOrderCreatedEvent>(message);

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