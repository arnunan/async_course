namespace Core.Session
{
    public interface ISessionFactory
    {
        Session GetSession(HttpContext context);
    }
}