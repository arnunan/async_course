using AuthService.Entities;

namespace AuthService.Authorization;

public interface ITokenHelper
{
    public string GenerateJwtToken(User user);
    
    public Guid? ValidateJwtToken(string? token);
}