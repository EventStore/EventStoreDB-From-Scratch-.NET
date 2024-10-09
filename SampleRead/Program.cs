using System.Text;
using EventStore.Client;

var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false");
await using var client = new EventStoreClient(settings);
	
var events = client.ReadStreamAsync(
  Direction.Forwards,
  "SampleStream",
  StreamPosition.Start
);

await foreach (var evt in events) {
  Console.WriteLine("************************");
  Console.WriteLine("You have read an event!");
  Console.WriteLine("Stream: " + evt.Event.EventStreamId);
  Console.WriteLine("Event Type: " + evt.Event.EventType);
  Console.WriteLine("Event Body: " + Encoding.UTF8.GetString(evt.Event.Data.ToArray()));
  Console.WriteLine("************************");
}