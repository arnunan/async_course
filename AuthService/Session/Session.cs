using Microsoft.AspNetCore.Mvc;

namespace AuthService.Session
{
    [ModelBinder(BinderType = typeof(SessionBinder))]
    public class Session
    {
        public Guid SessionId { get; set; }

        public Guid UserId { get; set; }

        public string AuthToken { get; set; }

        public int RoleId { get; set; }
    }
}