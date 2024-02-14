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

    public bool DisableMigrations => false;
}