using LastMinute.Exceptions;
using LastMinute.Services;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;

namespace LastMinute.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly ILastMinuteService _service;
        public DataController(ILastMinuteService service)
        {
            _service = service;
        }
        // GET api/data/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            JObject document = null;
           
            try 
            {
                document = _service.Get(id);
            }
            catch (DocumentNotFoundException)
            {
                return new HttpNotFoundResult();
            }
            
            return Ok(document);
        }

        // Boilerplate examples - How to do webapi
        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
