using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Core.Db.ContextSupport;

public class DbContextFactory<TDbContext> : IDbContextFactory<TDbContext>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    [NotNull] private readonly IDbSettings _settings;
    [NotNull] private readonly IDbContextCreator<TDbContext> _dbContextCreator;
    [NotNull] private readonly ILoggerFactory _loggerFactory;

    public DbContextFactory(
        [NotNull] IDbSettings settings,
        [NotNull] IDbContextCreator<TDbContext> dbContextCreator)
    {
        _settings = settings;
        _dbContextCreator = dbContextCreator;
        _loggerFactory = new LoggerFactory();
    }

    [NotNull]
    public TDbContext CreateDbContext() => _dbContextCreator.Create(_settings, _loggerFactory);
}