using DatabaseSecurity.Info;
using Microsoft.AspNetCore.Http;

namespace DatabaseSecurity.Middleware
{
    public class InfoSetterMiddleware
    {
        private readonly RequestDelegate _next;

        public InfoSetterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, IInfoSetter infoSetter)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var claims = context.User.Claims;
                infoSetter.SetUser(claims.ToDictionary(c => c.Type, c => (object)c.Value));
            }

            return _next(context);
        }
    }
}
