using Confluent.Kafka;

namespace Core.KafkaClient;

public sealed class MessageBus : IDisposable
{
    private readonly IProducer<Null, MessageContract> _producer;
    private IConsumer<Ignore, MessageContract> _consumer;
    private int _retryCount = 5;

    private readonly ConsumerConfig _consumerConfig;
    
    public MessageBus()
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost",
        };
        _consumerConfig = new ConsumerConfig
        {
            GroupId = "custom-group",
            BootstrapServers = "localhost"
        };

        _producer = new ProducerBuilder<Null, MessageContract>(producerConfig).Build();
    }

    public async void SendMessage(string topic, MessageContract message)
    {
        for (var i = 0; i < _retryCount; i++)
        {
            try
            {
                var kafkaMessage = new Message<Null, MessageContract> { Value = message };
                await _producer.ProduceAsync(topic, kafkaMessage, new CancellationToken(false));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var kafkaMessage = new Message<Null, MessageContract> { Value = message };
                if (i == _retryCount)
                    await _producer.ProduceAsync("error-topic", kafkaMessage, new CancellationToken(false));
            }
        }
    }

    public void SubscribeOnTopic<T>(string topic, Action<T> action, CancellationToken cancellationToken)
        where T : class
    {
        var msgBus = new MessageBus();
        using (msgBus._consumer = new ConsumerBuilder<Ignore, MessageContract>(_consumerConfig).Build())
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