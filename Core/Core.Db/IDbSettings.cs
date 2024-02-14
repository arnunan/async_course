using JetBrains.Annotations;

namespace Core.Db;

public interface IDbSettings
{
    [NotNull]
    string ConnectionString { get; }
    
    bool DisableMigrations { get; }
}