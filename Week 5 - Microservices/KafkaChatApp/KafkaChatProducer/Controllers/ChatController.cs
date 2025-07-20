using Confluent.Kafka;
using KafkaChatProducer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KafkaChatProducer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendMessage([FromBody] ChatMessage message)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var jsonMessage = JsonSerializer.Serialize(message);
                producer.Produce("chat-topic", new Message<Null, string> { Value = jsonMessage });
                producer.Flush(TimeSpan.FromSeconds(5));
            }

            return Ok("Message sent");
        }
    }
}
