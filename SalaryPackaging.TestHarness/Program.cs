using SalaryPackaging.Startup;
using SalaryPackaging.Service.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace SalaryPackaging.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Start();
            var demo = startup.Provider.GetRequiredService<IDemoSalaryPackaging>();
            demo.Show();
        }
    }
}