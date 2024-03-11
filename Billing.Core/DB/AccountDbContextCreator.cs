using Core.Db;
using Core.Db.ContextSupport;
using Microsoft.Extensions.Logging;

namespace Billing.Core.DB;

public class AccountDbContextCreator : IDbContextCreator<AccountDbContext>
{
    public AccountDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new AccountDbContext(settings, loggerFactory);
    }
}