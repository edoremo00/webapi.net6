using testidentityandjwt.BL.IMapper;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.BL.Services;

namespace testidentityandjwt.Startuphelper
{

        public static class StartupHelper //chiamati anche EXTENSION METHODS
        {
            public static IServiceCollection Configureservices(this IServiceCollection services)
            {

                services.AddTransient<IUserAuthService, UserAuthService>();
                services.AddTransient<IUserservice, Userservice>();
                services.AddTransient<Userservice>();
                services.AddTransient<fakeuserservicesameinterface>();
                services.AddTransient<IUserservice, fakeuserservicesameinterface>();
                services.AddTransient<Func<string, IUserservice>>(serviceProvider => key =>//TEST PER INIETTARE GIUSTA DIPENDENZA A RUNTIME SE PIù SERVICE
                                                                                           //IMPLEMENTANO STESSA INTERFACCIA PER CAPIRE QUALE TRA LE CLASSI CHE IMPLEMENTANO L'INTERFACCIA ANDARE A CHIAMARE
                {
                    switch (key)
                    {
                        case "fakeuserservicesameinterface":
                            return serviceProvider.GetService<fakeuserservicesameinterface>();
                        default: return serviceProvider.GetService<Userservice>();
                    }
                });
                services.AddSingleton<IDatamapper, Datamapper>();
                return services;


            }


        }
    
}
