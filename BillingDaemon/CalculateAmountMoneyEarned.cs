using Billing.Core.Models;
using Billing.Core.Services;
using Quartz;

namespace BillingDaemon;

public class CalculateAmountMoneyEarned : IJob
{
    private static IOperationsLogService _operationsLogService;
    private static ITotalMoneyEarnedService _totalMoneyEarnedService;

    public CalculateAmountMoneyEarned(
        IOperationsLogService operationsLogService,
        ITotalMoneyEarnedService totalMoneyEarnedService)
    {
        _operationsLogService = operationsLogService;
        _totalMoneyEarnedService = totalMoneyEarnedService;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var dateStart = DateTime.Now.Date;
        var dateEnd = DateTime.Now.Date.AddDays(1);
        var operationsByUsers = _operationsLogService.GetOperations(dateStart, dateEnd)
            .GroupBy(o => o.UserId);
        foreach (var operationsByUser in operationsByUsers)
        {
            var totalEarned = operationsByUser
                .Select(o => o.Amount)
                .Sum(o => o);
            //отправить на почту
            _totalMoneyEarnedService.AddTotalMoneyEarned(new TotalMoneyEarned
            {
                Date = dateStart,
                Amount = totalEarned
            });
        }

        return Task.CompletedTask;
    }
}