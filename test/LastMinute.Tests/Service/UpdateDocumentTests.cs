using Xunit;
using Newtonsoft.Json.Linq;

using LastMinute.Services;

namespace LastMinute.Tests
{
	public class Tests.Service
	{
		[Fact]
		public void ADocumentCanBeUpdatedById() 
		{
			// arrange
			ILastMinuteService sut = new LastMinuteService();
			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury},
			};
			sut.Create(document);
			
			// act
			string newInjury = "dicky ticker";
			JObject patch = new JObject {
				{"injury", newInjury}
			};
			sut.Patch(id, patch);
			
			// assert
			JObject response = sut.Get(id);
			
			Assert.Equal(newInjury, response["injury"]);
		}
	}
}
