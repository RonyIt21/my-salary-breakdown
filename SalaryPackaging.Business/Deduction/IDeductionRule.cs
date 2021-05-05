namespace SalaryPackaging.Business.Deduction
{
    internal interface IDeductionRule
    {
        Common.Deduction Calc(double taxableIncome);
    }
}
