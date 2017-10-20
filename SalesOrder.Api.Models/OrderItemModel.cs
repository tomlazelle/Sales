using System;
using System.Collections.Generic;
using SalesOrder.Api.Models.Handlers;

namespace SalesOrder.Api.Models
{
    public class OrderItemModel : Resource
    {
        public Guid SalesOrderId { get; set; }
        public int Id { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal WholeSalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal DollarsOff { get; set; }
        public decimal DiscountPercent { get; set; }
        public IDictionary<string, object> Details { get; set; }
    }
}