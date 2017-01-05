using System;
using Sales.Common;

namespace Sales.Domain.Messages
{
    public class CreateReturnMessage
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string Sku { get; set; }
        public ReturnReasons Reason { get; set; }
        public ReturnAction Action { get; set; }
        public string Note { get; set; }        
    }
}