using DatabaseSecurity.Locks;
using DatabaseSecurity.UnitOfWork;
using DatabaseSecurity.UnitTests.Context;
using DatabaseSecurity.UnitTests.Locks;
using DatabaseSecurity.UnitTests.MockData;
using DatabaseSecurity.UnitTests.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseSecurity.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblyOf<IMockData>()
            .AddClasses((classes) => classes.AssignableToAny(typeof(IMockData)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

            services.AddDbContext<TestContext>(options =>
            {
                options.UseInMemoryDatabase("test");
            });

            services.RegisterDatabaseSecurity();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IProtectedTestRepository, ProtectedTestRepository>();

            services.AddScoped<IProtected, ClientLock>();

            //Seed Data
            var provider = services.BuildServiceProvider();

            var ctx = provider.GetRequiredService<TestContext>();
            MockSeeder.Seed(ctx, provider.GetServices<IMockData>());
        }
    }
}
