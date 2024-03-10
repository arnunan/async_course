using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthService.Session
{
    public class SessionBinder : IModelBinder
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionBinder(ISessionFactory sessionFactory) =>
            _sessionFactory = sessionFactory;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var session = _sessionFactory.GetSession(bindingContext.HttpContext);
            bindingContext.Result = ModelBindingResult.Success(session);
            return Task.CompletedTask;
        }
    }
}