using Confluent.Kafka;
using PriceService.Models;
using System.Text.Json;

public class KafkaConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public KafkaConsumer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "price-history-service",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe("price-changed");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);

                Console.WriteLine($"Получили сообщение: {result.Message.Value}");

                var priceChanged = JsonSerializer.Deserialize<PriceChangedEvent>(result.Message.Value);

                if (priceChanged is null)
                    continue;

                using var scope = _scopeFactory.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var history = new PriceHistory
                {
                    ProductId = priceChanged.ProductId,
                    OldPrice = priceChanged.OldPrice,
                    NewPrice = priceChanged.NewPrice,
                    ChangedAt = DateTime.UtcNow
                };

                db.PriceHistories.Add(history);

                await db.SaveChangesAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Kafka Consumer: {ex.Message}");
            }
        }

        consumer.Close();
    }
}