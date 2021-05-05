using System.Linq;
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

        public virtual IList<Common.Deduction> GetDeductions(double taxableIncome)
        {
            return (from deductionRule in deductionRules
                    let deduction = deductionRule.Calc(taxableIncome)
                    select deduction).ToList();
        }
    }
}
