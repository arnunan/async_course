using AuthService.Exceptions;
using AuthService.Tokens;

namespace AuthService.Session
{
    public class SessionFactory : ISessionFactory
    {
        private readonly ITokenReaderWriter _tokenReaderWriter;

        public SessionFactory(ITokenReaderWriter tokenReaderWriter) =>
            _tokenReaderWriter = tokenReaderWriter;

        public Session GetSession(HttpContext context)
        {
            var token = _tokenReaderWriter.GetAndSaveToken(context);

            context.Request.Cookies.TryGetValue("authToken", out var authCookie);
            var authToken = !string.IsNullOrEmpty(authCookie)
                ? authCookie
                : string.Empty;
            if (string.IsNullOrEmpty(authToken))
                throw new AuthSessionRottenException();

            if (token == null)
                throw new SessionRottenException();

            if (authToken == null || token.AuthToken == null)
            {
                _tokenReaderWriter.RemoveToken(context);
                throw new SessionRottenException();
            }

            return BuildSessionFromToken(token);
        }

        private static Session BuildSessionFromToken(Token token) =>
            new()
            {
                AuthToken = token.AuthToken,
                RoleId = token.RoleId,
                SessionId = token.SessionId,
                UserId = token.UserId,
            };
    }
}