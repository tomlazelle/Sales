using System;
using System.Collections.Generic;
using Sales.Api.Models.Handlers;
using Sales.Common;

namespace Sales.Api.Models
{
    public class SalesOrderModel:Resource
    {
        public Guid AccountId { get; private set; }
        public AddressModel ShippingAddress { get; private set; }
        public AddressModel BillingAddress { get; private set; }
        public PersonModel Customer { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Total { get; private set; }
        public decimal DollarsOff { get; private set; }
        public decimal DiscountPercent { get; private set; }

        public IList<CustomerReturnModel> Returns { get; set; }

        public IList<OrderItemModel> Items { get; set; }

        public SalesOrderStatus Status { get; private set; }
        public SalesOrderTypes OrderType { get; private set; }
        public Guid RefNo { get; private set; }
    }
}