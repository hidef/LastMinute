using LastMinute.Models;
using System.Collections.Generic;

namespace LastMinute.Services
{
    public interface IPersister
    {
        void Append(IEvent e);
        IList<IEvent> Load();
    }
}