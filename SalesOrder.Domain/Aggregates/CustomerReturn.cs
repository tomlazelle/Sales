using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sales.Common;

namespace Sales.Domain.Aggregates
{
    public class CustomerReturn
    {
        
        public CustomerReturn(int id, decimal amount, int quantity, 
            string sku, ReturnReasons reason, ReturnAction action, List<string> notes, DateTime returnDate, 
            string returnId,
            ReturnStatus status)
        {            
            Id = id;
            Amount = amount;
            Quantity = quantity;
            Sku = sku;
            Reason = reason;
            Action = action;
            _notes = notes;
            ReturnDate = returnDate;
            ReturnId = returnId;
            Status = status;
        }

        private List<string> _notes = new List<string>();

        public string ReturnId { get; }
        public int Id { get; }
        public decimal Amount { get; }
        public int Quantity { get; }
        public string Sku { get; }
        public ReturnReasons Reason { get; }
        public ReturnAction Action { get; }

        public IList<string> Notes
        {
            get
            {
                if (_notes == null) return _notes = new List<string>();
                return _notes.AsReadOnly();
            }
        }

        public DateTime ReturnDate { get; }
        public ReturnStatus Status { get; }
    }
}