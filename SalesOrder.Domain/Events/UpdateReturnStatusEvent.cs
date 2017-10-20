using System;
using EventSource.Framework;
using SalesOrder.Common;

namespace SalesOrder.Domain.Events
{
    public class UpdateReturnStatusEvent:VersionedEvent<Guid>
    {
        public UpdateReturnStatusEvent(Guid id,ReturnStatus status, string returnId)
        {
            SourceId = id;
            Status = status;
            ReturnId = returnId;
        }
        public ReturnStatus Status { get; }
        public string ReturnId { get; }
    }
}