using System;

namespace LastMinute.Exceptions
{
    public class DocumentNotFoundException : Exception 
    {
        public DocumentNotFoundException(string documentId)
            : base(string.Format("Document with id [{0}] was not found.", documentId))
        {
        
        }        
    }
}