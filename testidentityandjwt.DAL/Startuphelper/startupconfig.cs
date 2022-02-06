using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.IServices;
using testidentityandjwt.DAL.Repository;
using testidentityandjwt.DAL.Services;

namespace testidentityandjwt.DAL.Startuphelper
{
    public static class StartupHelper //chiamati anche EXTENSION METHODS
    {
       public static IServiceCollection Configureservices(this IServiceCollection services)
        {
            //services.AddScoped<Userepo>();
            services.AddTransient<IUserAuthService, UserAuthService>();
            services.AddTransient<IUserservice,Userservice>();
            services.AddTransient<fakeuserservicesameinterface>();
           // services.AddTransient<IUserservice,fakeuserservice>();
            services.AddTransient<IUserservice,fakeuserservicesameinterface>();
            services.AddTransient <Func<string, IUserservice>>(serviceProvider => key =>//TEST PER INIETTARE GIUSTA DIPENDENZA A RUNTIME SE PIù SERVICE
            //IMPLEMENTANO STESSA INTERFACCIA PER CAPIRE QUALE TRA LE CLASSI CHE IMPLEMENTANO L'INTERFACCIA ANDARE A CHIAMARE
              {
                  switch (key)
                  {
                      case "fakeuserservicesameinterface":
                          return serviceProvider.GetService<fakeuserservicesameinterface>();
                      default: return serviceProvider.GetService<Userservice>();
                  }
              });
            return services;
        }
    }
}
