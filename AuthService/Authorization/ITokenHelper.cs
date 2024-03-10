using AuthService.Entities;
using AuthService.Tokens;

namespace AuthService.Authorization;

public interface ITokenHelper
{
    public string GenerateJwtToken(User user);
    
    public Guid? ValidateJwtToken(string? token);

    Token GetJwtToken(string cookie);

    string Encode(Token token);
}