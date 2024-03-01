using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Core.Db.ContextSupport;

public abstract class DesignTimeDbContextFactoryBase<TDbContext, TDbContextCreator> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    where TDbContextCreator: IDbContextCreator<TDbContext>, new()
{
    private readonly IDbContextCreator<TDbContext> _dbContextCreator;

    protected DesignTimeDbContextFactoryBase()
    {
        _dbContextCreator = new TDbContextCreator();
    }

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

        public string ConnectionString { get; }
        
        public string AuthConnectionString { get; }
        
        public string RoleConnectionString { get; }

        public bool DisableMigrations { get; } = false;
    }
}