using Xunit;
using Newtonsoft.Json.Linq;

using LastMinute.Services;

namespace LastMinute.Tests
{
	public class GetDocumentTests
	{
		[Fact]
		public void DocumentsCanBeRetrievedById() 
		{
			// arrange
			ILastMinuteService sut = new LastMinuteService();
			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};
			
			JObject document2 = new JObject {
				{"id", "jill"}, 
				{"injury", "unspecified"}
			};
		
			// act
			sut.Create(document);
			sut.Create(document2);	
			
			// assert
			JObject response = sut.Get(id);
			
			Assert.NotNull(response);
			Assert.Equal(id, response["id"]);
			Assert.Equal(injury, response["injury"]);
		}
	}
}
