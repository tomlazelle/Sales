using System;
using Sales.Common;

namespace Sales.Domain.Aggregates
{
    public class Payment
    {
        public Payment(PaymentTypes paymentType, decimal amount, DateTime paymentDate, string payeeName, PaymentStatus status)
        {
            PaymentType = paymentType;
            Amount = amount;
            PaymentDate = paymentDate;
            PayeeName = payeeName;
            Status = status;
        }
        public PaymentTypes PaymentType { get;  }
        public decimal Amount { get;  }
        public DateTime PaymentDate { get;  }
        public string PayeeName { get;}
        public PaymentStatus Status { get;  }
    }
}