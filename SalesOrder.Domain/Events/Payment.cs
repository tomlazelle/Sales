using System;
using Sales.Common;

namespace Sales.Domain.Events
{
    public class Payment
    {
        public Payment(PaymentTypes paymentType, decimal amount, DateTime paymentDate, string payeeName)
        {
            PaymentType = paymentType;
            Amount = amount;
            PaymentDate = paymentDate;
            PayeeName = payeeName;
        }
        public PaymentTypes PaymentType { get;  }
        public decimal Amount { get;  }
        public DateTime PaymentDate { get;  }
        public string PayeeName { get;}
    }
}