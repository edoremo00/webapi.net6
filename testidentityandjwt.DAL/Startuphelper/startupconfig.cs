using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.Context;
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
