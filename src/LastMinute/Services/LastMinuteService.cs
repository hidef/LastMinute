using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

using LastMinute.Exceptions;
using LastMinute.Models;

namespace LastMinute.Services
{
    public class LastMinuteService : ILastMinuteService
    {
        private Queue<IEvent> _events;
        
        private const string _ID_ = "id";
        
        private readonly IPersister _persister;
        
        public LastMinuteService()
        {
            _persister = new BasicPersister();
        }
        public LastMinuteService(IPersister persister)
        {
            _persister = persister;
        }
        
        public void Create(JObject document)
        {
            var e = new CreateEvent(document[_ID_].Value<string>(), document);
            _persister.Append(e);
            if ( _events != null )
            {
                _events.Enqueue(e);
            }
        }
        
        public void Patch(string id, JObject patchDocument)
        {
            var e = new PatchEvent(id, patchDocument);
            _persister.Append(e);
            if ( _events != null )
            {
                _events.Enqueue(e);
            }
        }
        
        public JObject Get(string id)
        {
            if ( _events == null )
            {
                _events = new Queue<IEvent>(_persister.Load());
            }
            
            JObject result = null;
            
            foreach ( IEvent e in _events )
            {
                if ( e.DocumentId == id )
                {
                    result = mergeEvent(result, e);
                }
            }
            
            if ( result == null )
            {
                throw new DocumentNotFoundException(id);
            }
            
            return result;
        }
        
        public void Delete(string id)
        {
            var e = new DeleteEvent(id);
            _persister.Append(e);
            if ( _events != null )
            {
                _events.Enqueue(e);
            }
        }
        
        private JObject mergeEvent(JObject document, IEvent e)
        {
            if ( e is CreateEvent )
            {
                CreateEvent createEvent = (CreateEvent) e;
                return createEvent.Document;    
            } 
            else if ( e is PatchEvent )
            {
                PatchEvent patchEvent = (PatchEvent) e;
                return patchDocument(document, patchEvent.Document);
            }
            else if ( e is DeleteEvent )
            {
                return null;
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