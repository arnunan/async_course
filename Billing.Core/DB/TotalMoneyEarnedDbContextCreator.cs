using Core.Db;
using Core.Db.ContextSupport;
using Microsoft.Extensions.Logging;

namespace Billing.Core.DB;

public class TotalMoneyEarnedDbContextCreator : IDbContextCreator<TotalMoneyEarnedDbContext>
{
    public TotalMoneyEarnedDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new TotalMoneyEarnedDbContext(settings, loggerFactory);
    }
}