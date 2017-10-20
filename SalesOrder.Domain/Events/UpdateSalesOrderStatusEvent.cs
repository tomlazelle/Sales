using System;
using EventSource.Framework;
using SalesOrder.Common;

namespace SalesOrder.Domain.Events
{
    public class UpdateSalesOrderStatusEvent:VersionedEvent<Guid>
    {
        public UpdateSalesOrderStatusEvent(Guid id,SalesOrderStatus status)
        {
            SourceId = id;
            Status = status;
        }

        public SalesOrderStatus Status { get;  }
    }
}