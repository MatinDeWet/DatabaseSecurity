using DatabaseSecurity.Identity;
using DatabaseSecurity.Info;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseSecurity
{
    public static class ServiceRegistration
    {
        public static void RegisterDatabaseSecurity(this IServiceCollection services)
        {
            services.AddScoped<IIdentityInfo, IdentityInfo>();
            services.AddScoped<IInfoSetter, InfoSetter>();
        }
    }
}
