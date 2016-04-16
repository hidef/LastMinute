using Xunit;
using Newtonsoft.Json.Linq;

using LastMinute.Services;

namespace LastMinute.Tests.Service
{
	public class CreateDocumentsTests
	{
		[Fact]
		public void ANewDocumentIsAdded() 
		{
			// arrange
			ILastMinuteService sut = new LastMinuteService();
			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};
		
			// act
			sut.Create(document);	
			
			// assert
			JObject response = sut.Get(id);
			
			Assert.NotNull(response);
			Assert.Equal(id, response["id"]);
			Assert.Equal(injury, response["injury"]);
		}
	}
}
