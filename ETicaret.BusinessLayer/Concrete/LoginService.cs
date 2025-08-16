using ETicaret.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ETicaret.BusinessLayer.Concrete // Namespace'i düzenledim
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserID
        {
            get
            {
                var user = _contextAccessor.HttpContext?.User;

                if (user?.Identity == null || !user.Identity.IsAuthenticated)
                    return null;

                // Önce NameIdentifier'a bak, yoksa "sub" claim'ine bak
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub");
                return userIdClaim?.Value;
            }
        }
    }
}