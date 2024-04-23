// See https://aka.ms/new-console-template for more information
using System.Text;
using EventStore.Client;

Console.WriteLine("Hello, World!");

var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false");
await using var client = new EventStoreClient(settings);
await AppendToStream(client);
await ReadFromStream(client);

return;

static async Task AppendToStream(EventStoreClient client) {
	#region append-to-stream

	var eventData = new EventData(
		Uuid.NewUuid(),
		"some-event",
		"{\"id\": \"1\" \"value\": \"some value\"}"u8.ToArray()
	);

	await client.AppendToStreamAsync(
		"SampleContent",
		StreamState.Any,
		new List<EventData> {
			eventData
		}
	);

	#endregion append-to-stream
}

static async Task ReadFromStream(EventStoreClient client) {
	#region read-from-stream

	var events = client.ReadStreamAsync(
		Direction.Forwards,
		"SampleContent",
		StreamPosition.Start
	);

	#endregion read-from-stream

	#region iterate-stream

	await foreach (var @event in events) Console.WriteLine(Encoding.UTF8.GetString(@event.Event.Data.ToArray()));

	#endregion iterate-stream

	#region #read-from-stream-positions

	Console.WriteLine(events.FirstStreamPosition);
	Console.WriteLine(events.LastStreamPosition);

	#endregion
}

static async Task ReadFromStreamMessages(EventStoreClient client) {
	#region read-from-stream-messages

	var streamPosition = StreamPosition.Start;
	var results = client.ReadStreamAsync(
		Direction.Forwards,
		"some-stream",
		streamPosition
	);

	#endregion read-from-stream-messages

	#region iterate-stream-messages

	await foreach (var message in results.Messages)
		switch (message) {
			case StreamMessage.Ok ok:
				Console.WriteLine("Stream found.");
				break;

			case StreamMessage.NotFound:
				Console.WriteLine("Stream not found.");
				return;

			case StreamMessage.Event(var resolvedEvent):
				Console.WriteLine(Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span));
				break;

			case StreamMessage.FirstStreamPosition(var sp):
				Console.WriteLine($"{sp} is after {streamPosition}; updating checkpoint.");
				streamPosition = sp;
				break;

			case StreamMessage.LastStreamPosition(var sp):
				Console.WriteLine($"The end of the stream is {sp}");
				break;

			default:
				break;
		}

	#endregion iterate-stream-messages
}

