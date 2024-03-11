using AuthService.DB;

namespace AuthService.Service;

public interface IRoleService
{
    RoleDbo GetRole(int roleId);
    
    RoleDbo[] GetRoles();
}