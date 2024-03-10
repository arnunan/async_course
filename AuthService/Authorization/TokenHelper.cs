using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Entities;
using AuthService.Settings;
using AuthService.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Authorization;

public class TokenHelper : ITokenHelper
{
    private readonly AppSettings _appSettings;

    public TokenHelper(AppSettings appSettings)
    {
        _appSettings = appSettings;

        if (string.IsNullOrEmpty(_appSettings.Secret))
            throw new Exception("JWT secret not configured");
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("authToken", Guid.NewGuid().ToString()),
                new Claim("roleId", user.RoleId.ToString()),
                new Claim("sessionId", Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString())
            }),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string Encode(Token token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("authToken", token.AuthToken),
                new Claim("roleId", token.RoleId.ToString()),
                new Claim("sessionId", token.SessionId.ToString()),
                new Claim("userId", token.UserId.ToString())
            }),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        return tokenHandler.CreateEncodedJwt(tokenDescriptor);
    }

    public Guid? ValidateJwtToken(string? token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }

    public Token GetJwtToken(string cookie)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
        try
        {
            tokenHandler.ValidateToken(cookie, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var token = new Token
            {
                AuthToken = jwtToken.Claims.First(x => x.Type == "authToken").Value,
                RoleId = int.Parse(jwtToken.Claims.First(x => x.Type == "roleId").Value),
                SessionId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "sessionId").Value),
                UserId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value),
            };
            return token;
        }
        catch
        {
            return null;
        }
    }
}