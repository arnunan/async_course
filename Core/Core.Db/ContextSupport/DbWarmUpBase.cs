using Core.WarmUp;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Core.Db.ContextSupport;

public abstract class DbWarmUpBase<TDbContext> : IWarmUp 
    where TDbContext : DbContext
{
    [NotNull] private readonly IDbContextFactory<TDbContext> _dbContextFactory;
    [NotNull] private readonly IDbSettings _settings;

    public DbWarmUpBase(
        [NotNull] IDbContextFactory<TDbContext> dbContextFactory, 
        [NotNull] IDbSettings settings)
    {
        _dbContextFactory = dbContextFactory;
        _settings = settings;
    }

    public async Task RunAsync()
    {
        await using var context = _dbContextFactory.CreateDbContext();
        if (_settings.DisableMigrations)
        {
            await context.Database.EnsureCreatedAsync();
        }
        else
        {
            await context.Database.MigrateAsync();
        }
    }
}