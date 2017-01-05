using System;
using EventSource.Framework;
using Sales.Common;

namespace Sales.Domain.Events
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