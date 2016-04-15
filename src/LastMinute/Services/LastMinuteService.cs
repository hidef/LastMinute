using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

using LastMinute.Models;

namespace LastMinute.Services
{
    public class LastMinuteService : ILastMinuteService
    {
        private readonly Queue<IEvent> _events = new Queue<IEvent>();
        
        private const string _ID_ = "id";
        
        public void Create(JObject document)
        {
            _events.Enqueue(new CreateEvent(document[_ID_].Value<string>(), document));
        }
        
        public void Patch(string id, JObject patchDocument)
        {
            _events.Enqueue(new PatchEvent(id, patchDocument));
        }
        
        public JObject Get(string id)
        {
            JObject result = null;
            
            foreach ( IEvent e in _events )
            {
                if ( e.DocumentId == id )
                {
                    result = mergeEvent(result, e);
                }
            }
            
            return result;
        }
        
        private JObject mergeEvent(JObject document, IEvent e)
        {
            if ( e is CreateEvent )
            {
                CreateEvent createEvent = (CreateEvent) e;
                return createEvent.Document;    
            } 
            else if ( e is PatchEvent)
            {
                PatchEvent patchEvent = (PatchEvent) e;
                return patchDocument(document, patchEvent.Document);
            }
            else
            {
                throw new Exception("Unmergable event encountered.");
            }
        }
        
        private JObject patchDocument(JObject original, JObject patch)
        {
            foreach ( KeyValuePair<string, JToken> kvp in patch )
            {
                original[kvp.Key] = kvp.Value;
            }
            
            return original;
        }
    }
}