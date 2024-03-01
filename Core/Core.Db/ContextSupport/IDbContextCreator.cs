using Microsoft.Extensions.Logging;

namespace Core.Db.ContextSupport;

public interface IDbContextCreator<out TDbContext>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    TDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory);
}