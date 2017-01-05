using System.Collections.Generic;

namespace Sales.Domain.Events
{
    public class CreateOrderItemEvent
    {
        public CreateOrderItemEvent(string sku, int quantity, decimal wholeSalePrice, decimal retailPrice, decimal dollarsOff, decimal discountPercent, IDictionary<string, object> details)
        {
            Sku = sku;
            Quantity = quantity;
            WholeSalePrice = wholeSalePrice;
            RetailPrice = retailPrice;
            DollarsOff = dollarsOff;
            DiscountPercent = discountPercent;
            Details = details;
        }
        public string Sku { get;}
        public int Quantity { get;}
        public decimal WholeSalePrice { get;}
        public decimal RetailPrice { get;}
        public decimal DollarsOff { get;}
        public decimal DiscountPercent { get;}
        public IDictionary<string, object> Details { get;}
    }
}