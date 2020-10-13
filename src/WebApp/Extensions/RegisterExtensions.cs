using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Business.Abstract.Services;
using WebApp.Business.Services;
using WebApp.Core.Constants;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Uow;

namespace WebApp.Extensions
{
    internal static class RegisterExtensions
    {
        internal static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(AppSettings.DefaultConnection);
            services.AddDbContext<WebAppDbContext>(options => { options.UseSqlServer(connectionString); });
        }

        internal static void AddDependencyResolvers(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISampleService, SampleService>();
        }
    }
}