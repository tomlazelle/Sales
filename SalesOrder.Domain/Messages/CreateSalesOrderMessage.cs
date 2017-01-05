using System;
using System.Collections.Generic;
using Sales.Common;

namespace Sales.Domain.Messages
{
    public class CreateSalesOrderMessage
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public AddressMessage ShippingAddress { get; set; }
        public AddressMessage BillingAddress { get; set; }
        public PersonMessage Customer { get; set; }
        public PaymentMessage Payment { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public decimal DollarsOff { get; set; }
        public decimal DiscountPercent { get; set; }
        public SalesOrderStatus Status { get; set; }
        public SalesOrderTypes OrderType { get; set; }
        public Guid RefNo { get; set; }
        public IList<CreateSalesOrderItemMessage> Items { get; set; }
    }
}