using Core.Db;
using Core.Db.ContextSupport;
using Microsoft.Extensions.Logging;

namespace Billing.Core.DB;

public class MostExpensiveTaskDbContextCreator : IDbContextCreator<MostExpensiveTasksDbContext>
{
    public MostExpensiveTasksDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new MostExpensiveTasksDbContext(settings, loggerFactory);
    }
}