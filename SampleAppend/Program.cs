using System.Text;
using EventStore.Client;

var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false");
await using var client = new EventStoreClient(settings);

var eventType = "SampleEventType";
var eventData = new EventData(
    Uuid.NewUuid(),
    eventType,
    "{\"id\": \"1\", \"value\": \"some value\"}"u8.ToArray() // Fixed JSON format
);

var eventStream = "SampleStream";
await client.AppendToStreamAsync(
    eventStream,
    StreamState.Any,
    new List<EventData> { eventData }
);

Console.WriteLine("************************");
Console.WriteLine("🎉 Congratulations, you have written an event!");
Console.WriteLine("Stream: " + eventStream);
Console.WriteLine("Event Type: " + eventType);
Console.WriteLine("Event Body: " + Encoding.UTF8.GetString(eventData.Data.ToArray()));
Console.WriteLine("************************");
