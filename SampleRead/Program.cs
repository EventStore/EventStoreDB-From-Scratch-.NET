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

///////////////////////////////////////////
//
// Step 2. Read all events from the stream
//
///////////////////////////////////////////

var events = client.ReadStreamAsync(  // Read events from stream
  Direction.Forwards,                 // Read events forward in time
  "SampleStream",                     // Name of stream to read from
  StreamPosition.Start                // Read from the start of the stream
);

///////////////////////////////////////
//
// Step 3. Print each event to console
//
///////////////////////////////////////

await foreach (var evt in events) {                                   // For each event in stream
  Console.WriteLine("************************");                      //
  Console.WriteLine("You have read an event!");                       //
  Console.WriteLine("Stream: " + evt.Event.EventStreamId);            // Print the stream name of the event
  Console.WriteLine("Event Type: " + evt.Event.EventType);            // Print the type of the event
  Console.WriteLine("Event Body: " + Encoding.UTF8.GetString(         // Print the body of the event. convert the byte array to string
                                        evt.Event.Data.ToArray()));   // Gets the event body as a byte array
  Console.WriteLine("************************");
}