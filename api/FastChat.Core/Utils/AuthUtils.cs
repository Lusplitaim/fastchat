using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FastChat.Core.Utils
{
    public class AuthUtils : IAuthUtils
    {
        private readonly IHttpContextAccessor m_HttpContextAccessor;

        public AuthUtils(IHttpContextAccessor httpContextAccessor)
        {
            m_HttpContextAccessor = httpContextAccessor;
        }

        public int GetAuthUserId()
        {
            string id = m_HttpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Convert.ToInt32(id);
        }

        public string GetAuthUserName()
        {
            return m_HttpContextAccessor.HttpContext.User.Identity!.Name!;
        }
    }
}
