using LastMinute.Services;
using NSubstitute;
using Xunit;
using Microsoft.AspNet.Mvc;
using LastMinute.Controllers;
using LastMinute.Exceptions;
using Newtonsoft.Json.Linq;

namespace LastMinute.Tests.Controller
{
    public class DataControllerCreationTests 
    {
        [Fact]
        public void GettingAMissingDocumentReturnsA404() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();
            service.Get(Arg.Any<string>()).Returns(x => { throw new DocumentNotFoundException("xxx"); });
            DataController controller = new DataController(service);
            
            IActionResult result = controller.Get("some_random_id");  
              
            Assert.IsType(typeof(HttpNotFoundResult), result);
        }
        
        [Fact]
        public void WhenADocumentExistsA200OKIsReturned() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();
            string id = "jack";
            string injury = "broken crown";
            JObject document = new JObject {
                {"id", id},
                {"injury", injury}
            };
            service.Get(Arg.Any<string>()).Returns(x => document);
            DataController controller = new DataController(service);
            
            IActionResult result = controller.Get("some_random_id");  
              
            Assert.IsType(typeof(HttpOkObjectResult), result);
            HttpOkObjectResult okResult = (HttpOkObjectResult) result;
            
            Assert.True(okResult.StatusCode.HasValue);
            Assert.Equal(200, okResult.StatusCode.Value);
        }
        
        [Fact]
        public void GettingADocumentByIdThatHasBeenPersistedReturnsTheDocument() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();
            string id = "jack";
            string injury = "broken crown";
            JObject document = new JObject {
                {"id", id},
                {"injury", injury}
            };
            service.Get(Arg.Any<string>()).Returns(x => document);
            DataController controller = new DataController(service);
            
            IActionResult result = controller.Get("some_random_id");  
              
            Assert.IsType(typeof(HttpOkObjectResult), result);
            HttpOkObjectResult okResult = (HttpOkObjectResult) result;
            
            Assert.IsType(typeof(JObject), okResult.Value);
            JObject responseDocument = (JObject) okResult.Value;
            Assert.Equal(id, responseDocument["id"].Value<string>());
            Assert.Equal(injury, responseDocument["injury"].Value<string>());
        }
    }
}