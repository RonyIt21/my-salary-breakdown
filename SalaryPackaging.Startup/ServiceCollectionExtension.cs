using System.Linq;
using SalaryPackaging.Common;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SalaryPackaging.Startup
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDictionary<TOptions>(this IServiceCollection services, IConfigurationSection section) where TOptions : class, IDictionary<string, string>
        { 
            if (section == null) 
                return services.Configure<TOptions>(x => { });

            var values = section.GetChildren().ToList();

            return services.Configure<TOptions>(x => values.ForEach(v => x.Add(v.Key, v.Value)));
        }

        public static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services.ConfigureDictionary<AppSetting>(configuration.GetSection(typeof(AppSetting).Name));
        }
    }
}
