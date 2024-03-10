namespace Core.Db;

public interface IDbSettings
{
    string ConnectionString { get; }
    
    bool DisableMigrations { get; }
}