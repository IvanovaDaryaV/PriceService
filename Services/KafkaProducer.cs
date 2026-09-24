using Confluent.Kafka;
using PriceService.Models;
using System.Text.Json;

public class KafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task SendPriceChangedAsync(PriceChangedEvent priceChanged)
    {
        var message = JsonSerializer.Serialize(priceChanged);

        await _producer.ProduceAsync(
            "price-changed",        // название topic
            new Message<Null, string>
            {
                Value = message
            });
    }
}