using JetBrains.Annotations;

namespace Core.Db.ContextSupport;

public interface IDbContextFactory<out TDbContext> 
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    [NotNull]
    TDbContext CreateDbContext();
}