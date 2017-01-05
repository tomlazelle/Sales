using System;

namespace Sales.Domain.Messages
{
    public class AddReturnNoteMessage
    {
        public Guid Id { get; set; }
        public string ReturnId { get; set; }
        public string Note { get; set; }
    }
}