using Billing.Core.DB;
using Billing.Core.Models;
using ContextSupport = Core.Db.ContextSupport;

namespace Billing.Core.Services;

public class MostExpensiveTasksService : IMostExpensiveTasksService
{
    private readonly MostExpensiveTasksDbContext _tasksDbContext;

    public MostExpensiveTasksService(ContextSupport.IDbContextFactory<MostExpensiveTasksDbContext> taskDbContextFactory)
    {
        _tasksDbContext = taskDbContextFactory.CreateDbContext();
    }

    public MostExpensiveTasksModel Get(DateTime date)
    {
        var mostExpensiveTask = _tasksDbContext.Tasks.FirstOrDefault(t => t.Date == date);
        return new MostExpensiveTasksModel
        {
            Date = mostExpensiveTask.Date,
            PrefixId = mostExpensiveTask.PrefixId,
            Id = mostExpensiveTask.Id,
            Cost = mostExpensiveTask.Cost
        };
    }

    public MostExpensiveTasksModel[] Get(DateTime dateStart, DateTime dateEnd)
    {
        var mostExpensiveTask = _tasksDbContext.Tasks.Where(t => t.Date > dateStart && t.Date < dateEnd);
        return mostExpensiveTask.Select(t => new MostExpensiveTasksModel
        {
            Date = t.Date,
            PrefixId = t.PrefixId,
            Id = t.Id,
            Cost = t.Cost
        }).ToArray();
    }

    public void Add(MostExpensiveTasksModel mostExpensiveTasksModel)
    {
        _tasksDbContext.Add(mostExpensiveTasksModel);
    }
}