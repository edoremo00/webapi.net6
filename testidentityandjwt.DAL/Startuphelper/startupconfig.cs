using Microsoft.Extensions.DependencyInjection;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.DAL.Startuphelper
{
    public static class StartupHelper
    {
       public static IServiceCollection Configureservices(this IServiceCollection services)
        {
            services.AddScoped<Userepo>();
            return services;
        }
    }
}
