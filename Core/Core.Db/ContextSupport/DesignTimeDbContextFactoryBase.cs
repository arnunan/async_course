using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Core.Db.ContextSupport;

public abstract class DesignTimeDbContextFactoryBase<TDbContext, TDbContextCreator> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    where TDbContextCreator: IDbContextCreator<TDbContext>, new()
{
    [NotNull] private readonly IDbContextCreator<TDbContext> _dbContextCreator;

    protected DesignTimeDbContextFactoryBase()
    {
        _dbContextCreator = new TDbContextCreator();
    }

    [NotNull]
    public TDbContext CreateDbContext(string[] args)
    {
        var loggerFactory = new LoggerFactory();
        var dbSettings = new MigrationDbSettings(MigrationDatabaseName);
        return _dbContextCreator.Create(dbSettings, loggerFactory);
    }

    protected abstract string MigrationDatabaseName { get; }
    
    private class MigrationDbSettings : IDbSettings
    {
        public MigrationDbSettings(string databaseName)
        {
            ConnectionString = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Username = "postgres",
                Password = "",
                Database = databaseName,
            }.ToString();
        }

        [NotNull] public string ConnectionString { get; }

        public bool DisableMigrations { get; } = false;
    }
}