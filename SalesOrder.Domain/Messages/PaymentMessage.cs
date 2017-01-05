using System;
using Sales.Common;

namespace Sales.Domain.Messages
{
    public class PaymentMessage
    {
        public PaymentTypes PaymentType { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PayeeName { get; set; }
    }
}