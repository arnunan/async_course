namespace Core.Db.ContextSupport;

public interface IDbContextFactory<out TDbContext> 
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    TDbContext CreateDbContext();
}