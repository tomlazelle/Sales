using System;
using System.Collections.Generic;
using SalesOrder.Api.Models.Handlers;
using SalesOrder.Common;

namespace SalesOrder.Api.Models
{
    public class CustomerReturnModel:Resource
    {
        public Guid SalesOrderId { get; set; }
        public string ReturnId { get; set; }
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string Sku { get; set; }
        public ReturnReasons Reason { get; set; }
        public ReturnAction Action { get; set; }
        public IList<string> Notes { get; set; }
        public DateTime ReturnDate { get; set; }
        public ReturnStatus Status { get; set; }
    }
}