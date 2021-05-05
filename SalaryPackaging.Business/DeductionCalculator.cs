using SalaryPackaging.Common;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using SalaryPackaging.Business.Contract;
using SalaryPackaging.Business.Deduction;

namespace SalaryPackaging.Business
{
    /// <summary>
    /// Evaluator knows all rules
    /// </summary>
    public class DeductionCalculator : IDeductionCalculator
    {
        private readonly IList<IDeductionRule> deductionRules = new List<IDeductionRule>();

        public DeductionCalculator(IOptions<AppSetting> settings)
        {
            var taxYear = int.Parse(settings.Value["TaxYear"]);
            deductionRules.Add(new BudgetRepairLevyRule(taxYear));
            deductionRules.Add(new MedicareLevyRule(taxYear));
            deductionRules.Add(new IncomeTaxRule(taxYear));
        }

        public IList<Common.Deduction> GetDeductions(double taxableIncome)
        {
            var deductions = new List<Common.Deduction>();

            foreach(var deductionRule in deductionRules)
            {
                var deduction = deductionRule.Calc(taxableIncome);
                deductions.Add(deduction);
            }

            return deductions;
        }
    }
}
