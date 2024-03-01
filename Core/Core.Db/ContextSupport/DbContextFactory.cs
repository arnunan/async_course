using Microsoft.Extensions.Logging;

namespace Core.Db.ContextSupport;

public class DbContextFactory<TDbContext> : IDbContextFactory<TDbContext>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IDbSettings _settings;
    private readonly IDbContextCreator<TDbContext> _dbContextCreator;
    private readonly ILoggerFactory _loggerFactory;

    public DbContextFactory(
        IDbSettings settings,
        IDbContextCreator<TDbContext> dbContextCreator)
    {
        _settings = settings;
        _dbContextCreator = dbContextCreator;
        _loggerFactory = new LoggerFactory();
    }

    public TDbContext CreateDbContext() => _dbContextCreator.Create(_settings, _loggerFactory);
}