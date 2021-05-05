using SalaryPackaging.Core.Interface;

namespace SalaryPackaging.Core
{
    public class NoTracer : ITracer
    {
        public void Log(string message)
        {
            // do nothing
        }
    }
}
