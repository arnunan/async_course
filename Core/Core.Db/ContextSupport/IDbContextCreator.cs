using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Core.Db.ContextSupport;

public interface IDbContextCreator<out TDbContext>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    TDbContext Create([NotNull] IDbSettings settings, [NotNull] ILoggerFactory loggerFactory);
}