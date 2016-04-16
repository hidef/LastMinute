using LastMinute.Services;
using NSubstitute;
using Xunit;
using Microsoft.AspNet.Mvc;
using LastMinute.Controllers;

namespace LastMinute.Tests.Controller
{
    public class DataControllerDeletionTests 
    {
        [Fact]
        public void WhenADocumentExistsOKIsReturned() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();
            DataController controller = new DataController(service);
            
            IActionResult result = controller.Delete("some_random_id");  
              
            Assert.IsType(typeof(HttpOkResult), result);
        }
        
        [Fact]
        public void TheDocumentIsDeletedFromTheService() 
        {
            ILastMinuteService service = Substitute.For<ILastMinuteService>();

            DataController controller = new DataController(service);
            string id = "some_id";
            
            IActionResult result = controller.Delete(id);  
              
            service.Received(1).Delete(id);
        }
    }
}