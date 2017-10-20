using System;
using EventSource.Framework;

namespace Sales.Domain.Events
{
    public class AddReturnNoteEvent:VersionedEvent<Guid>
    {
        public AddReturnNoteEvent(Guid id,string note, string returnId)
        {
            SourceId = id;
            Note = note;
            ReturnId = returnId;
        }
        public string Note { get;  }
        public string ReturnId { get;  }
    }
}