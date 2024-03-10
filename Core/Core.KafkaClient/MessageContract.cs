namespace Core.KafkaClient;

public class MessageContract
{
    public MessageContract(object message)
    {
        MessageId = Guid.NewGuid();
        Message = message;
    }

    public Guid MessageId { get; set; }
    
    public object Message { get; set; }
}