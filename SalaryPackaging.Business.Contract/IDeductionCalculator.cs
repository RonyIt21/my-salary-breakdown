using System.Collections.Generic;

namespace SalaryPackaging.Business.Contract
{
    public interface IDeductionCalculator
    {
        IList<Common.Deduction> GetDeductions(double taxableIncome);
    }
}
