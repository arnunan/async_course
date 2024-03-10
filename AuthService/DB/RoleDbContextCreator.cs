using Core.Db;
using Core.Db.ContextSupport;

namespace AuthService.DB;

public class RoleDbContextCreator : IDbContextCreator<RoleDbContext>
{
    public RoleDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new RoleDbContext(settings, loggerFactory);
    }
}