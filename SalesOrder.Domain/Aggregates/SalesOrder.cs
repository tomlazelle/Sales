using System;
using System.Collections.Generic;
using System.Linq;
using EventSource.Framework;
using Sales.Common;
using Sales.Domain.Events;
using Sales.Domain.Handlers;

namespace Sales.Domain.Aggregates
{
    public class SalesOrder : BaseEntity<Guid>
    {
        private List<OrderItem> _items = new List<OrderItem>();
        private List<CustomerReturn> _returns = new List<CustomerReturn>();

        public SalesOrder(Guid id)
            : base(id)
        {
            Handles<SalesOrderCreatedEvent>(NewSalesOrder);
            Handles<UpdateSalesOrderStatusEvent>(StatusChanged);
            Handles<CreateReturnEvent>(ReturnCreated);
            Handles<UpdateReturnStatusEvent>(ClaimStatusChanged);
            Handles<AddReturnNoteEvent>(ReturnNoteAdded);
        }

        private void ReturnNoteAdded(AddReturnNoteEvent addReturnNoteEvent)
        {
            var claim = _returns.FirstOrDefault(x => x.ReturnId == addReturnNoteEvent.ReturnId);

            _returns.Remove(claim);

            var notes = claim.Notes.ToList();

            notes.Add(addReturnNoteEvent.Note);

            _returns.Add(new CustomerReturn(claim.Id,
                claim.Amount,
                claim.Quantity,
                claim.Sku,
                claim.Reason,
                claim.Action,
                notes.ToList(),
                claim.ReturnDate,
                claim.ReturnId,
                claim.Status));
        }

        public SalesOrder(Guid id, SalesOrderEvents eventItems)
            : this(id)
        {
            LoadEvents(eventItems.Events);
        }

        public Guid AccountId { get; private set; }
        public Address ShippingAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public Person Customer { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Total { get; private set; }
        public decimal DollarsOff { get; private set; }
        public decimal DiscountPercent { get; private set; }

        public IList<CustomerReturn> Returns
        {
            get
            {
                if (_returns == null) return _returns = new List<CustomerReturn>();

                return _returns.AsReadOnly();
            }
        }

        public IList<OrderItem> Items
        {
            get
            {
                if (_items == null) return _items = new List<OrderItem>();

                return _items.AsReadOnly();
            }
        }

        public SalesOrderStatus Status { get; private set; }
        public SalesOrderTypes OrderType { get; private set; }
        public Guid RefNo { get; private set; }

        private void ClaimStatusChanged(UpdateReturnStatusEvent updateReturnStatus)
        {
            var claim = _returns.FirstOrDefault(x => x.ReturnId == updateReturnStatus.ReturnId);

            _returns.Remove(claim);

            _returns.Add(new CustomerReturn(claim.Id,
                claim.Amount,
                claim.Quantity,
                claim.Sku,
                claim.Reason,
                claim.Action,
                claim.Notes.ToList(),
                claim.ReturnDate,
                claim.ReturnId,
                updateReturnStatus.Status));
        }

        private void ReturnCreated(CreateReturnEvent createReturnEvent)
        {
            var id = _returns.Count + 1;
            _returns.Add(new CustomerReturn(id,
                createReturnEvent.Amount,
                createReturnEvent.Quantity,
                createReturnEvent.Sku,
                createReturnEvent.Reason,
                createReturnEvent.Action,
                new List<string>
                {
                    createReturnEvent.Note
                },
                createReturnEvent.ReturnDate,
                createReturnEvent.ReturnId,
                createReturnEvent.Status));
        }

        private void StatusChanged(UpdateSalesOrderStatusEvent updateSalesOrderStatusEvent)
        {
            Status = updateSalesOrderStatusEvent.Status;
        }

        private void NewSalesOrder(SalesOrderCreatedEvent salesOrderCreatedEvent)
        {
            AccountId = salesOrderCreatedEvent.AccountId;
            ShippingAddress = salesOrderCreatedEvent.ShippingAddress;
            BillingAddress = salesOrderCreatedEvent.BillingAddress;
            Customer = salesOrderCreatedEvent.Customer;
            OrderDate = salesOrderCreatedEvent.OrderDate;
            SubTotal = salesOrderCreatedEvent.SubTotal;
            Tax = salesOrderCreatedEvent.Tax;
            Total = salesOrderCreatedEvent.Total;
            DollarsOff = salesOrderCreatedEvent.DollarsOff;
            DiscountPercent = salesOrderCreatedEvent.DiscountPercent;
            Status = SalesOrderStatus.Open;

            _items = new List<OrderItem>();

            foreach (var itemEvent in salesOrderCreatedEvent.Items)
            {
                var id = Items.Count + 1;
                _items.Add(new OrderItem(
                    id,
                    itemEvent.Sku,
                    itemEvent.Quantity,
                    itemEvent.WholeSalePrice,
                    itemEvent.RetailPrice,
                    itemEvent.DollarsOff,
                    itemEvent.DiscountPercent,
                    itemEvent.Details));
            }
        }
    }
}