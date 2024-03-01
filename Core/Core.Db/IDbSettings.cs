namespace Core.Db;

public interface IDbSettings
{
    string ConnectionString { get; }
    
    string AuthConnectionString { get; }
    
    string RoleConnectionString { get; }
    
    bool DisableMigrations { get; }
}