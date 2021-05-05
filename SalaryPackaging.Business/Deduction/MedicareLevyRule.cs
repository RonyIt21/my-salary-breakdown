using System.Collections.Generic;

namespace SalaryPackaging.Business.Deduction
{
    internal class MedicareLevyRule : DeductionRuleBase
    {
        internal MedicareLevyRule(int taxYear) : base(taxYear)
        {
        }

        protected override string DisplayName => "Medicare Levy";

        protected override Dictionary<int, Dictionary<int, double>> GetAllRules()
        {
            return new Dictionary<int, Dictionary<int, double>>
            {
                {
                    2021, new Dictionary<int, double>
                    {
                        { 0, 0 },
                        { 21336, 10 },
                        { 26669, 2 }
                    }
                }
            };
        }
    }
}
