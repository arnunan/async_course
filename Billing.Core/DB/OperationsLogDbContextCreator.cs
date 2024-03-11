using Core.Db;
using Core.Db.ContextSupport;
using Microsoft.Extensions.Logging;

namespace Billing.Core.DB;

public class OperationsLogDbContextCreator : IDbContextCreator<OperationsLogDbContext>
{
    public OperationsLogDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new OperationsLogDbContext(settings, loggerFactory);
    }
}