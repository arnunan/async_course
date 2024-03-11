using Billing.Core.Models;
using Billing.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillingExternalApi;

[ApiController]
[Route("externalapi/billing")]
public class ExternalApiController : ControllerBase
{
    private static IOperationsLogService _operationsLogService;
    private static IAccountService _accountService;
    private static IMostExpensiveTasksService _mostExpensiveTasksService;

    public ExternalApiController(IOperationsLogService operationsLogService,
        IAccountService accountService,
        IMostExpensiveTasksService mostExpensiveTasksService)
    {
        _operationsLogService = operationsLogService;
        _accountService = accountService;
        _mostExpensiveTasksService = mostExpensiveTasksService;
    }

    [HttpGet("operation")]
    public Operation[] GetOperationLog(Guid userId)
    {
        return _operationsLogService.GetOperations(userId);
    }

    [HttpGet("current-balance")]
    public int GetCurrentBalance(Guid userId)
    {
        return _accountService.GetBalance(userId);
    }

    [HttpGet("red-employees")]
    public int GetRedEmployees()
    {
        return _accountService.GetNegativeBalanceCount();
    }

    [HttpGet("most-expensive-task-of-day")]
    public Guid GetMostExpensiveTaskOfDay()
    {
        return _mostExpensiveTasksService.Get(DateTime.Today).Id;
    }

    [HttpGet("most-expensive-task-of-week")]
    public Guid GetMostExpensiveTaskOfWeek()
    {
        var startOfWeek = StartOfWeek(DateTime.Today, DayOfWeek.Monday);
        var endOfWeek = StartOfWeek(DateTime.Today, DayOfWeek.Monday).AddDays(7);
        return _mostExpensiveTasksService.Get(startOfWeek, endOfWeek).DistinctBy(t => t.Cost).First().Id;
    }

    [HttpGet("most-expensive-task-of-month")]
    public Guid GetMostExpensiveTaskOfMonth()
    {
        var now = DateTime.Now;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        var endOfMonth = new DateTime(now.Year, now.Month + 1, 1).AddTicks(-1);
        return _mostExpensiveTasksService.Get(startOfMonth, endOfMonth).DistinctBy(t => t.Cost).First().Id;
    }

    private static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
    {
        var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}