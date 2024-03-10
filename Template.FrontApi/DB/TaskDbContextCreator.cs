using Core.Db;
using Core.Db.ContextSupport;

namespace Template.FrontApi.DB;

public class UserDbContextCreator : IDbContextCreator<TasksDbContext>
{
    public TasksDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new TasksDbContext(settings, loggerFactory);
    }
}