using System.Collections.Generic;

namespace SalaryPackaging.Business.Deduction
{
    internal class BudgetRepairLevyRule : DeductionRuleBase, IDeductionRule
    {
        internal BudgetRepairLevyRule(int taxYear) : base(taxYear)
        {
        }

        protected override string DisplayName => "Budget Repair Levy";

        protected override Dictionary<int, Dictionary<int, double>> GetAllRules()
        {
            return new Dictionary<int, Dictionary<int, double>>
            {
                {
                    2021, new Dictionary<int, double>
                    {
                       { 0, 0 },
                       { 180001, 2 }
                    }
                }
            };
        }
    }
}
