namespace Core.Db.Configuration;

public class DbSettings<TAppProperties> : IDbSettings
    where TAppProperties : IDbProperties
{
    public string ConnectionString
    {
        get
        {
            const string databaseName = "postgres";
            const string userName = "postgres";
            const string password = "123456";
            const string host = "localhost";
            const int port = 5432;

            return $"Server={host}; " + $"Port={port}; " +
                   $"User Id={userName};" + $"Password={password};" + $"Database={databaseName};";
        }
    }

    public bool DisableMigrations => false;
}