using System;
using System.IO;
using SalaryPackaging.Core;
using SalaryPackaging.Service;
using SalaryPackaging.Business;
using SalaryPackaging.Core.Interface;
using SalaryPackaging.Business.Contract;
using SalaryPackaging.Service.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SalaryPackaging.Startup
{
    public class Start
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public Start()
        {
            _configuration = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                              .Build();

            var services = RegisterServices()
                           .AddSingleton<IConfiguration>(_configuration)
                           .AddOptions()
                           .ConfigureAppSettings(_configuration);

            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        ///  This is intentionally protected as derived startup could be used mocking few or all of dependencies i.e MockStartup
        ///  Instead of having one seperate virtual methods i.e. RegisterBusiness, RegisterService, RegisterCore etc.
        /// </summary>
        protected virtual IServiceCollection RegisterServices()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddTransient<ITracer, NoTracer>();
            collection.AddTransient<IExceptionHandler, ExceptionHandler>();
            collection.AddTransient<IDeductionCalculator, DeductionCalculator>();
            collection.AddTransient<IDeductionCalculator, DeductionCalculator>();
            collection.AddTransient<IProvideSalaryBreakdown, SalaryBreakdownComponent>();
            collection.AddTransient<IFormatSalaryBreakdown, DisplaySalaryBreakdown>();
            collection.AddTransient<IDemoSalaryPackaging, RunSalaryPackaging>();
            return collection;
        }

        public IServiceProvider Provider => _serviceProvider;

        public IConfiguration Configuration => _configuration;
    }
}
