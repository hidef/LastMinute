using Newtonsoft.Json.Linq;

namespace LastMinute.Services
{
    public interface ILastMinuteService
    {
        void Create(JObject document);
        JObject Get(string id);   
    }
}