using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.IServices;
using testidentityandjwt.DAL.Repository;
using testidentityandjwt.DAL.Services;

namespace testidentityandjwt.DAL.Startuphelper
{
    public static class StartupHelper
    {
       public static IServiceCollection Configureservices(this IServiceCollection services)
        {
            //services.AddScoped<Userepo>();
            services.AddTransient<IUserAuthService, UserAuthService>();
            services.AddTransient<IUserservice, Userservice>();
            return services;
        }
    }
}
