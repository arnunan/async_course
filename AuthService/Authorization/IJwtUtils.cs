using AuthService.Entities;

namespace AuthService.Authorization;

public interface IJwtUtils
{
    public string GenerateJwtToken(User user);
    
    public Guid? ValidateJwtToken(string? token);
}