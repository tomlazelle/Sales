using System.Collections.Generic;

namespace Sales.Domain.Messages
{
    public class CreateSalesOrderItemMessage
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal WholeSalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal DollarsOff { get; set; }
        public decimal DiscountPercent { get; set; }
        public IDictionary<string, object> Details { get; set; }
    }
}