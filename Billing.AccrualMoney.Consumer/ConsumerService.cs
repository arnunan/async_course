using Billing.Core.Models;
using Billing.Core.Services;
using Core.KafkaClient;
using Microsoft.Extensions.Hosting;

namespace Billing.AccrualMoney.Consumer;

public class ConsumerService : IHostedService
{
    private static MessageBus? _msgBus;
    private static IAccountService _accountService;
    private static IOperationsLogService _operationsLogService;

    public ConsumerService(IAccountService accountService, IOperationsLogService operationsLogService)
    {
        _accountService = accountService;
        _operationsLogService = operationsLogService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (_msgBus = new MessageBus())
            while (true)
            {
                _msgBus.SubscribeOnTopic<string>("finished-task", FinishedTaskCommandListener, CancellationToken.None);
            }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static void FinishedTaskCommandListener(string messageUserId)
    {
        var userId = Guid.Parse(messageUserId);
        var taskId = Guid.Parse(messageUserId);
        var random = new Random();
        var taskCost = random.Next(20, 40);
        _accountService.UpdateAccount(userId, taskCost);
        _operationsLogService.AddOperations(new Operation
        {
            UserId = userId,
            TaskId = taskId,
            Status = OperationStatus.Ascending,
            Amount = taskCost
        });
    }
}