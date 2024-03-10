using Billing.Core.Models;
using Billing.Core.Services;
using Quartz;

namespace BillingDaemon;

public class CalculateMostExpensiveTask : IJob
{
    private static ITasksService _tasksService;
    private static IMostExpensiveTasksService _mostExpensiveTasksService;

    public CalculateMostExpensiveTask(ITasksService tasksService, IMostExpensiveTasksService mostExpensiveTasksService)
    {
        _tasksService = tasksService;
        _mostExpensiveTasksService = mostExpensiveTasksService;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var tasks = _tasksService.GetTasks(DateTime.Today);
        var mostExpensiveTask = tasks.Select(t => (t.Id, t.PrefixId, t.Cost)).DistinctBy(t => t.Cost).Last();
        _mostExpensiveTasksService.Add(new MostExpensiveTasksModel
        {
            Date = DateTime.Today,
            PrefixId = mostExpensiveTask.PrefixId,
            Id = mostExpensiveTask.Id,
            Cost = mostExpensiveTask.Cost
        });
        return Task.CompletedTask;
    }
}