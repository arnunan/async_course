using Template.FrontApi.Clients;
using Template.FrontApi.DB;
using Template.FrontApi.Models;
using ContextSupport = Core.Db.ContextSupport;
using TaskStatus = Template.FrontApi.DB.TaskStatus;

namespace Template.FrontApi.Service;

public class AccountService : IAccountService
{
    private readonly TasksDbContext _tasksDbContext;
    private readonly BillingApiClient _billingApiClient;

    public AccountService(ContextSupport.IDbContextFactory<TasksDbContext> taskDbContextFactory,
        BillingApiClient billingApiClient)
    {
        _billingApiClient = billingApiClient;
        _tasksDbContext = taskDbContextFactory.CreateDbContext();
    }

    public async Task<Operation[]?> GetOperationLog(Guid userId)
    {
        return await _billingApiClient.GetOperationLog(userId);
    }

    public async Task<decimal> GetCurrentBalance(Guid userId)
    {
        return await _billingApiClient.GetCurrentBalance(userId);
    }

    public int GetTodayTotalCost()
    {
        var assignedTaskSum = _tasksDbContext.Tasks
            .Where(t => t.Status == TaskStatus.InProgress)
            .Sum(t => t.Cost);
        var finishedTaskSum = _tasksDbContext.Tasks
            .Where(t => t.Status == TaskStatus.Done)
            .Sum(t => t.Cost);
        return assignedTaskSum - finishedTaskSum;
    }
}