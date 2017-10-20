using System;
using Raven.Imports.Newtonsoft.Json;
using SalesOrder.Common;

namespace SalesOrder.Domain.Messages
{
    public class UpdateReturnStatusMessage
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public ReturnStatus Status { get; set; }
        public string ReturnId { get; set; }
    }
}