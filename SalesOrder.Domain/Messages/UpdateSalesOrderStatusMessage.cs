using System;
using SalesOrder.Common;

namespace SalesOrder.Domain.Messages
{
    public class UpdateSalesOrderStatusMessage
    {
        public Guid Id { get; set; }
        public SalesOrderStatus Status { get; set; }
    }
}