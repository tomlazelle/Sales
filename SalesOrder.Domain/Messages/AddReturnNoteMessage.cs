using System;

namespace SalesOrder.Domain.Messages
{
    public class AddReturnNoteMessage
    {
        public Guid Id { get; set; }
        public string ReturnId { get; set; }
        public string Note { get; set; }
    }
}