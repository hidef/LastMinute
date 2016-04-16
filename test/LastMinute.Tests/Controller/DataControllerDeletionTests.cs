using LastMinute.Services;
using NSubstitute;
using Xunit;
using Microsoft.AspNet.Mvc;
using LastMinute.Controllers;
using LastMinute.Exceptions;
using Newtonsoft.Json.Linq;

namespace LastMinute.Tests.Controller
{
    public class DataControllerDeletionTests 
    {
        [Fact]
        public void WhenADocumentExistsOKIsReturned() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();
            string id = "jack";
            string injury = "broken crown";
            JObject document = new JObject {
                {"id", id},
                {"injury", injury}
            };
            DataController controller = new DataController(service);
            
            IActionResult result = controller.Delete("some_random_id");  
              
            Assert.IsType(typeof(HttpOkResult), result);
        }
        
        [Fact]
        public void TheDocumentIsDeletedFromTheService() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();
            string id = "jack";
            string injury = "broken crown";
            JObject document = new JObject {
                {"id", id},
                {"injury", injury}
            };

            DataController controller = new DataController(service);
            
            IActionResult result = controller.Delete(id);  
              
            service.Received(1).Delete(id);
        }
    }
}