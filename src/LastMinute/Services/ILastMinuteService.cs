using Newtonsoft.Json.Linq;

namespace LastMinute.Services
{
    public interface ILastMinuteService
    {
        void Create(JObject document);
        
        void Patch(string id, JObject patchDocument);
        JObject Get(string id);   
        
        void Delete(string id);
    }
}