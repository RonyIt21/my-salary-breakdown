using SalaryPackaging.Common;

namespace SalaryPackaging.Service.Contract
{
    /// <summary>
    /// we can switch various formatter for different display
    /// </summary>
    public interface IFormatSalaryBreakdown
    {
        void Print(SalaryDetail salaryDetail);
    }
}
