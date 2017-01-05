using System;
using Sales.Common;

namespace Sales.Domain.Messages
{
    public class UpdateReturnStatusMessage
    {
        public Guid Id { get; set; }
        public ReturnStatus Status { get; set; }
        public string ReturnId { get; set; }
    }
}