using Newtonsoft.Json.Linq;
 
namespace LastMinute.Models
{
    public class PatchEvent : IEvent 
    {
        private readonly string _documentId;
        private readonly JObject _document;
        
        public PatchEvent(string documentId, JObject document)
        {
            _documentId = documentId;
            _document = document;
        }
        
        public string DocumentId 
        {
            get 
            { 
                return _documentId;    
            }
        }
        
        public JObject Document 
        {
            get 
            {
                return _document;
            }
        }
    }
}