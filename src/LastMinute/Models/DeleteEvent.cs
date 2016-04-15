using Newtonsoft.Json.Linq;

namespace LastMinute.Models
{
    public class DeleteEvent : IEvent 
    {
        private readonly string _documentId;
        
        public DeleteEvent(string documentId)
        {
            _documentId = documentId;
        }
        
        public string DocumentId 
        {
            get 
            { 
                return _documentId;    
            }
        }
    }
}