using LastMinute.Models;
using System.Collections.Generic;

namespace LastMinute.Services
{
    public class BasicPersister : IPersister
    {
        private readonly Queue<IEvent> _events = new Queue<IEvent>();
        public void Append(IEvent e)
        {
            _events.Enqueue(e);
        }
        public IList<IEvent> Load()
        {
            return new List<IEvent>(_events);   
        }
    }
}