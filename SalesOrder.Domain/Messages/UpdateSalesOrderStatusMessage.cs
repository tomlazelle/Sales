using System;
using Sales.Common;

namespace Sales.Domain.Messages
{
    public class UpdateSalesOrderStatusMessage
    {
        public Guid Id { get; set; }
        public SalesOrderStatus Status { get; set; }
    }
}