using Billing.Core.Services;
using Quartz;

namespace BillingDaemon;

public class DebtBalance : IJob
{
    private static IAccountService _accountService;

    public DebtBalance(IAccountService accountService)
    {
        _accountService = accountService;
    }


    public Task Execute(IJobExecutionContext context)
    {
        _accountService.ResetAccounts();
        return Task.CompletedTask;
    }
}