using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services.IWebServices;
using WebApp.Services.WebServices;

namespace WebApp.Services
{
    public static class WebServiceCollection
    {
        public static IServiceCollection AddWebService(this IServiceCollection services)
        {
            services.AddTransient<IAppAccessLog, AppAccessLog>();
            services.AddTransient<ITextLogWrite, TextLogWrite>();
            services.AddTransient<IUploaderHelper, FileUploadHelper>();

            return services;
        }
    }
}
