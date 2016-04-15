using Xunit;
using Newtonsoft.Json.Linq;

using LastMinute.Services;
using NSubstitute;
using LastMinute.Models;
using System.Collections.Generic;

namespace LastMinute.Tests
{
	public class PersistenceTests
	{
		[Fact]
		public void CreationsArePersisted() 
		{
			// arrange
			IPersister persister = Substitute.For<IPersister>();

			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};

			ILastMinuteService sut = new LastMinuteService(persister);
			sut.Create(document);	

			persister.Received(1).Append(Arg.Is<CreateEvent>(e => e.DocumentId == id && e.Document == document));
		}
		
		[Fact]
		public void CreationsAreLoadedFromPersister() 
		{
			// arrange
			IPersister persister = Substitute.For<IPersister>();

			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};
			
			persister.Load().Returns(new List<IEvent> {
				new CreateEvent(id, document)
			});
			
			ILastMinuteService sut = new LastMinuteService(persister);
			JObject retrievedDocument = sut.Get(id);	

			Assert.Equal(id, retrievedDocument["id"].Value<string>());
			Assert.Equal(injury, retrievedDocument["injury"].Value<string>());
		}

		[Fact]
		public void CreationPersistBetweenRuns() 
		{
			// arrange
			IPersister persister = new BasicPersister();

			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};

			ILastMinuteService firstSut = new LastMinuteService(persister);
			firstSut.Create(document);	
			
			ILastMinuteService secondSut = new LastMinuteService(persister);
			JObject response = secondSut.Get(id);
			
			// assert
			Assert.NotNull(response);
			Assert.Equal(id, response["id"]);
			Assert.Equal(injury, response["injury"]);
		}
	}
}
