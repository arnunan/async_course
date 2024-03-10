namespace AuthService.Session
{
    public interface ISessionFactory
    {
        Session GetSession(HttpContext context);
    }
}