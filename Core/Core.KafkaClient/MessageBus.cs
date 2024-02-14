using Confluent.Kafka;

namespace Core.KafkaClient;

public sealed class MessageBus : IDisposable
{
    private readonly IProducer<Null, string> _producer;
    private IConsumer<Ignore, string> _consumer;

    private readonly ConsumerConfig _consumerConfig;

    public MessageBus() : this("localhost")
    {
    }

    public MessageBus(string host)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = host,
        };
        _consumerConfig = new ConsumerConfig
        {
            GroupId = "custom-group",
            BootstrapServers = host
        };

        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public async void SendMessage(string topic, string message)
    {
        var kafkaMessage = new Message<Null, string> { Value = message };
        await _producer.ProduceAsync(topic, kafkaMessage, new CancellationToken(false));
    }

    public void SubscribeOnTopic<T>(string topic, Action<T> action, CancellationToken cancellationToken)
        where T : class
    {
        var msgBus = new MessageBus();
        using (msgBus._consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
        {
            msgBus._consumer.Assign(new List<TopicPartitionOffset> { new(topic, 0, -1) });

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                var cancelled = false;
                while (!cancelled)
                {
                    try
                    {
                        var consumeResult = msgBus._consumer.Consume(TimeSpan.FromMilliseconds(10));
                        if (consumeResult?.Message?.Value == null)
                            continue;
                        action(consumeResult.Message.Value as T);
                        cancelled = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }
    }

    public void Dispose()
    {
        _producer?.Dispose();
        _consumer?.Dispose();
    }
}