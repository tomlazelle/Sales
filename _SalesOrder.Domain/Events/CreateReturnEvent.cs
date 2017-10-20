using System;
using EventSource.Framework;
using Sales.Common;

namespace Sales.Domain.Events
{
    public class CreateReturnEvent:VersionedEvent<Guid>
    {
        public CreateReturnEvent(Guid id, decimal amount, int quantity, 
            string sku, ReturnReasons reason, ReturnAction action, string note, DateTime returnDate,
            string returnId,
            ReturnStatus status)
        {
            SourceId = id;
            Amount = amount;
            Quantity = quantity;
            Sku = sku;
            Reason = reason;
            Action = action;
            Note = note;
            ReturnDate = returnDate;
            ReturnId = returnId;
            Status = status;
        }

        

        public string ReturnId;
        public decimal Amount { get; }
        public int Quantity { get; }
        public string Sku { get; }
        public ReturnReasons Reason { get; }
        public ReturnAction Action { get; }
        public string Note { get; }
        public DateTime ReturnDate { get; }
        public ReturnStatus Status { get; }
    }
}