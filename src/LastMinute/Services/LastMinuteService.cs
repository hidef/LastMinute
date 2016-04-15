using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace LastMinute.Services
{
    public class LastMinuteService : ILastMinuteService
    {
        private Dictionary<string, JObject> data = new Dictionary<string, JObject>();
        
        private const string _ID_ = "id";
        
        public void Create(JObject document)
        {
            data[document[_ID_].Value<string>()] = document;
        }
        
        public JObject Get(string id)
        {
            return data[id];
        }
    }
}