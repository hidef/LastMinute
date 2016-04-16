using Xunit;
using Newtonsoft.Json.Linq;

using LastMinute.Services;
using NSubstitute;
using LastMinute.Models;
using System.Collections.Generic;

namespace LastMinute.Tests.Service
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
		
		[Fact]
		public void UpdatesArePersisted() 
		{
			// arrange
			IPersister persister = Substitute.For<IPersister>();

			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};
			
			string newInjury = "dicky ticker";
			JObject patch = new JObject {
				{"injury", newInjury}
			};

			ILastMinuteService sut = new LastMinuteService(persister);
			sut.Create(document);	
			sut.Patch(id, patch);

			persister.Received(1).Append(Arg.Is<PatchEvent>(e => e.DocumentId == id && e.Document == patch));
		}
		
		[Fact]
		public void UpdatesAreLoadedFromPersister() 
		{
			// arrange
			IPersister persister = Substitute.For<IPersister>();

			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};
			
			string newInjury = "dicky ticker";
			JObject patch = new JObject {
				{"injury", newInjury}
			};
			
			persister.Load().Returns(new List<IEvent> {
				new CreateEvent(id, document),
				new PatchEvent(id, patch)
			});
			
			ILastMinuteService sut = new LastMinuteService(persister);
			JObject retrievedDocument = sut.Get(id);	

			Assert.Equal(id, retrievedDocument["id"].Value<string>());
			Assert.Equal(newInjury, retrievedDocument["injury"].Value<string>());
		}

		[Fact]
		public void UpdatesPersistBetweenRuns() 
		{
			// arrange
			IPersister persister = new BasicPersister();

			string id = "jack";
			string injury = "broken crown";
			JObject document = new JObject {
				{"id", id}, 
				{"injury", injury}
			};
			
			string newInjury = "dicky ticker";
			JObject patch = new JObject {
				{"injury", newInjury}
			};

			ILastMinuteService firstSut = new LastMinuteService(persister);
			firstSut.Create(document);	
			firstSut.Patch(id, patch);

			ILastMinuteService secondSut = new LastMinuteService(persister);
			JObject response = secondSut.Get(id);
			
			// assert
			Assert.NotNull(response);
			Assert.Equal(id, response["id"]);
			Assert.Equal(newInjury, response["injury"]);
		}
	}
}
