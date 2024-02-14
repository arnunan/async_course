using Core.KafkaClient;

namespace ServiceTemplate;

class ServiceProgram
{
    private static MessageBus? _msgBus;
    private const string FirstTopicNameResp = "first_topic";
    private const string SecondTopicNameResp = "second_topic";


    static void Main(string[] args)
    {
        var canceled = false;

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            canceled = true;
        };

        using (_msgBus = new MessageBus("localhost"))
        {
            _msgBus.SubscribeOnTopic<string>(FirstTopicNameResp, msg => FirstCommandListener(msg),
                CancellationToken.None);
            _msgBus.SubscribeOnTopic<string>(SecondTopicNameResp, msg => SecondCommandListener(msg),
                CancellationToken.None);
            while (!canceled)
            {
            }
        }
    }

    private static void FirstCommandListener(string msg)
    {
        var r = new Random().Next(10, 15);
        _msgBus?.SendMessage(FirstTopicNameResp, r.ToString());
        Console.WriteLine($"Sending {r.ToString()}");
    }

    private static void SecondCommandListener(string msg)
    {
        var r = new Random().Next(0, 5);
        _msgBus?.SendMessage(SecondTopicNameResp, r.ToString());
        Console.WriteLine($"Sending {r.ToString()}");
    }
}