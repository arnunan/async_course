namespace AuthService.Tokens
{
    public interface ITokenReaderWriter
    {
        Token? GetToken(HttpContext context);
        
        Token GetToken(Session.Session session);
        
        Token? GetAndSaveToken(HttpContext context);
        
        void RemoveToken(HttpContext context);

        void WriteCookie(HttpContext context, Token token);
    }
}