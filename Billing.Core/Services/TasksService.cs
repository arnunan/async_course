using Billing.Core.DB;
using Billing.Core.Models;
using ContextSupport = Core.Db.ContextSupport;

namespace Billing.Core.Services;

public class TasksService : ITasksService
{
    private readonly TasksDbContext _tasksDbContext;

    public TasksService(ContextSupport.IDbContextFactory<TasksDbContext> taskDbContextFactory)
    {
        _tasksDbContext = taskDbContextFactory.CreateDbContext();
    }
    
    public TaskModel[] GetTasks(DateTime date)
    {
        return _tasksDbContext.Tasks
            .Where(t => t.CreatedAt.Date == date)
            .Select(t => new TaskModel
            {
                PrefixId = t.PrefixId,
                Id = t.Id,
                Assigned = t.Assigned,
                Topic = t.Topic,
                Content = t.Content,
                CreatedAt = t.CreatedAt,
                Cost = t.Cost
            })
            .ToArray();
    }
}