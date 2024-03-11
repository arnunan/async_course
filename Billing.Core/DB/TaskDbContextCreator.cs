using Core.Db;
using Core.Db.ContextSupport;
using Microsoft.Extensions.Logging;

namespace Billing.Core.DB;

public class TaskDbContextCreator : IDbContextCreator<TasksDbContext>
{
    public TasksDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new TasksDbContext(settings, loggerFactory);
    }
}