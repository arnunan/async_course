using AuthService.DB;
using ContextSupport = Core.Db.ContextSupport;

namespace AuthService.Service;

public class RoleService : IRoleService
{
    private readonly RoleDbContext _roleDbContext;

    public RoleService(ContextSupport.IDbContextFactory<RoleDbContext> roleContextFactory) =>
        _roleDbContext = roleContextFactory.CreateDbContext();

    public RoleDbo? GetRole(int roleId) =>
        _roleDbContext.Roles.FirstOrDefault(u => u.RoleId == roleId);

    public RoleDbo[] GetRoles() =>
        _roleDbContext.Roles.ToArray();
}