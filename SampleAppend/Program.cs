using System.Text;
using EventStore.Client;

////////////////////////////////////////////////////////
//
// Step 1. Create client and connect it to EventStoreDB
//
////////////////////////////////////////////////////////

// Create an instance of EventStoreDBClient, connecting to the EventStoreDB at localhost without TLS
var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false");
await using var client = new EventStoreClient(settings);

/////////////////////////////////////////////////////////////
//
// Step 2. Create new event object with a type and data body
//
/////////////////////////////////////////////////////////////

var eventType = "SampleEventType";                              // Define the event type for the new event
var eventData = new EventData(                                  // Create a new event with a type and body
    Uuid.NewUuid(),                                             // Specify a new UUID for the event
    eventType,                                                  // Specify the event type
    @"{""id"": ""1"", ""value"": ""some value""}"u8.ToArray()   // Specify the event data body as a JSON in byte format
);

///////////////////////////////////////////////////
//
// Step 3. Append the event object into the stream
//
///////////////////////////////////////////////////

var eventStream = "SampleStream";  // Define the stream name where the event will be appended
await client.AppendToStreamAsync(  // Append the event to a stream
    eventStream,                   // Name of the stream to append the event to       
    StreamState.Any,               // Set to append regardless of the current stream state (you can ignore this for now)        
    [eventData]                    // The event to append (in a list)
);

///////////////////////////////////////////////
//
// Step 4. Print the appended event to console
//
///////////////////////////////////////////////

Console.WriteLine("************************");
Console.WriteLine("🎉 Congratulations, you have written an event!");
Console.WriteLine("Stream: " + eventStream);
Console.WriteLine("Event Type: " + eventType);
Console.WriteLine("Event Body: " + Encoding.UTF8.GetString(eventData.Data.ToArray()));
Console.WriteLine("************************");
