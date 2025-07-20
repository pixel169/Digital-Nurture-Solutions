using Confluent.Kafka;
using System.Text.Json;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "chat-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("chat-topic");

Console.WriteLine("Listening to chat messages...");
while (true)
{
    var consumeResult = consumer.Consume();
    var chat = JsonSerializer.Deserialize<ChatMessage>(consumeResult.Message.Value);
    Console.WriteLine($"{chat?.Sender}: {chat?.Message}");
}

public class ChatMessage
{
    public string Sender { get; set; }
    public string Message { get; set; }
}
