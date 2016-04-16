using LastMinute.Services;
using NSubstitute;
using Xunit;
using Microsoft.AspNet.Mvc;
using LastMinute.Controllers;
using Newtonsoft.Json.Linq;

namespace LastMinute.Tests.Controller
{
    public class DataControllerPutTests 
    {
        [Fact]
        public void OKIsReturned() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();
            string id = "jack";
            string injury = "broken crown";
            JObject document = new JObject {
                {"id", id},
                {"injury", injury}
            };
            DataController controller = new DataController(service);
            
            
            IActionResult result = controller.Put(id, document);  
              
            Assert.IsType(typeof(HttpOkResult), result);
        }
        
        [Fact]
        public void TheUpdateIsPassedToTheService() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();
            string id = "jack";
            string injury = "broken crown";
            JObject document = new JObject {
                {"id", id},
                {"injury", injury}
            };

            DataController controller = new DataController(service);
            
            IActionResult result = controller.Put(id, document);  
              
            service.Received(1).Patch(id, document);
        }
    }
}