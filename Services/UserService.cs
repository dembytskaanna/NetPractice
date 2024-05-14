using Cinema.Models;
using System.Security.Claims;

namespace Cinema.Services
{
    public class UserService : IUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetMyId()
        {
            var result = "";
            if (_httpContextAccessor != null) 
            { 
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            int id = Convert.ToInt32(result);
            return id;
            
        }

        public string GetMyRole()
        {
            var result = "";
            if (_httpContextAccessor != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;
        }
    }
}
