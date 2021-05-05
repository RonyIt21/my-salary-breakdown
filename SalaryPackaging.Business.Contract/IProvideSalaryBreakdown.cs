using SalaryPackaging.Common;

namespace SalaryPackaging.Business.Contract
{
    public interface IProvideSalaryBreakdown
    {
        SalaryDetail Get(double gross, PayFrequency payFrequency);
    }
}
