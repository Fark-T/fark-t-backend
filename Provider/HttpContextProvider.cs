using System.Security.Claims;

namespace fark_t_backend.Provider
{
    public class HttpContextProvider : IHttpContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUser()
        {
            var id = _httpContextAccessor.HttpContext?.User.FindFirstValue("userID");
            return !Guid.TryParse(id, out var userId) ? new Guid("00000000-0000-0000-0000-000000000000") : userId;
        }
    }
}
