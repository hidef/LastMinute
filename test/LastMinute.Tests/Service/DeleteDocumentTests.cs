using Xunit;
using Newtonsoft.Json.Linq;

using LastMinute.Services;
using LastMinute.Exceptions;

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
			DocumentNotFoundException ex = Assert.Throws<DocumentNotFoundException>(() => sut.Get(id));
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
			DocumentNotFoundException ex = Assert.Throws<DocumentNotFoundException>(() => sut.Get(id));
		}
	}
}