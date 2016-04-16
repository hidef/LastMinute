using Xunit;
using Newtonsoft.Json.Linq;

using LastMinute.Services;

namespace LastMinute.Tests.Service
{
	public class DeleteDocumentTests
	{
		[Fact]
		public void DocumentsCanBeDeletedById() 
		{
			// arrange
			ILastMinuteService sut = new LastMinuteService();
			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};
			sut.Create(document);
			
			// act
			sut.Delete(id);
			
			// assert
			JObject response = sut.Get(id);
			
			Assert.NotNull(response);
			Assert.Equal(id, response["id"]);
			Assert.Equal(injury, response["injury"]);
		}
		
		[Fact]
		public void DeleteOperationsAreIdempotent() 
		{
			// arrange
			ILastMinuteService sut = new LastMinuteService();
			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};
			sut.Create(document);
			
			// act
			sut.Delete(id);
			sut.Delete(id);
			
			// assert
			JObject response = sut.Get(id);
			
			Assert.NotNull(response);
			Assert.Equal(id, response["id"]);
			Assert.Equal(injury, response["injury"]);
		}
	}
}
}
}
