using AuthService.Authorization;

namespace AuthService.Tokens
{
    public class TokenReaderWriter : ITokenReaderWriter
    {
        private readonly ITokenHelper _tokenHelper;

        public TokenReaderWriter(ITokenHelper tokenHelper) =>
            _tokenHelper = tokenHelper;

        public Token? GetToken(HttpContext context)
        {
            var isCookieExists = context.Request.Cookies.TryGetValue("authToken", out var cookie);
            if (!isCookieExists)
                return null;
            return string.IsNullOrEmpty(cookie)
                ? null
                : _tokenHelper.GetJwtToken(cookie);
        }

        public Token GetToken(Session.Session session)
        {
            var token = new Token
            {
                AuthToken = session.AuthToken,
                RoleId = session.RoleId,
                SessionId = session.SessionId,
                UserId = session.UserId,
            };

            token.SetExpiration();

            return token;
        }

        public Token? GetAndSaveToken(HttpContext context)
        {
            var token = GetToken(context);
            if (token != null)
                WriteCookie(context, token);
            return token;
        }

        public void WriteCookie(HttpContext context, Token token)
        {
            token.SetExpiration();
            context.Response.Cookies.Append(
                "authToken",
                _tokenHelper.Encode(token),
                new CookieOptions
                {
                    Domain = "localhost.dev.course",
                    Expires = new DateTime(token.ExpirationTicks, DateTimeKind.Utc),
                    HttpOnly = true
                });
        }

        public void RemoveToken(HttpContext context)
        {
            context.Response.Cookies.Delete("authToken");
        }
    }
}