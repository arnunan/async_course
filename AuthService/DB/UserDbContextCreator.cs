using Core.Db;
using Core.Db.ContextSupport;

namespace AuthService.DB;

public class UserDbContextCreator : IDbContextCreator<UserDbContext>
{
    public UserDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new UserDbContext(settings, loggerFactory);
    }
}