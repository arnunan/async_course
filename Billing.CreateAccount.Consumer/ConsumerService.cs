using Billing.Core.Services;
using Core.KafkaClient;
using Microsoft.Extensions.Hosting;

namespace Billing.CreateAccount.Consumer;

public class ConsumerService : IHostedService
{
    private static MessageBus? _msgBus;
    private static IAccountService _accountService;

    public ConsumerService(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (_msgBus = new MessageBus())
            while (true)
            {
                _msgBus.SubscribeOnTopic<string>("sign-up", SignUpCommandListener, CancellationToken.None);
            }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static void SignUpCommandListener(string messageUserId)
    {
        var userId = Guid.Parse(messageUserId);
        _accountService.CreateAccount(userId);
        Console.WriteLine($"Sending {messageUserId}");
    }
}