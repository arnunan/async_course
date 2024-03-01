using System.Data.Common;
using Npgsql;

namespace Core.Db.Configuration;

public class DbSettings<TAppProperties> : IDbSettings
    where TAppProperties : IDbProperties
{
    private readonly TAppProperties _appProperties;

    public DbSettings(TAppProperties appProperties)
    {
        _appProperties = appProperties;
    }

    public string ConnectionString
    {
        get
        {
            var builder = new NpgsqlConnectionStringBuilder(_appProperties.ConnectionString)
            {
                Username = _appProperties.User,
                Password = _appProperties.Password
            };
            return builder.ToString();
        }
    }

    public string AuthConnectionString
    {
        get
        {
            var providerName = "Npgsql"; //Get this
            var databaseName = "Auth";
            var userName = "postgres";
            var password = "123456";
            var host = "localhost";
            var port = 5432;

            //Insert it here
            return $"Server={host}; " + $"Port={port}; " +
                   $"User Id={userName};" + $"Password={password};" + $"Database={databaseName};";

            var builder = new NpgsqlConnectionStringBuilder(_appProperties.AuthConnectionString)
            {
                Username = _appProperties.User,
                Password = _appProperties.Password
            };
            return builder.ToString();
        }
    }

    public string RoleConnectionString
    {
        get
        {
            var builder = new NpgsqlConnectionStringBuilder(_appProperties.RoleConnectionString)
            {
                Username = _appProperties.User,
                Password = _appProperties.Password
            };
            return builder.ToString();
        }
    }

    public bool DisableMigrations => false;
}