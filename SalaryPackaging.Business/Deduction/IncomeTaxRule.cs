using System.Collections.Generic;

namespace SalaryPackaging.Business.Deduction
{
    internal class IncomeTaxRule : DeductionRuleBase
    {
        internal IncomeTaxRule(int taxYear) : base(taxYear)
        {
        }

        protected override string DisplayName => "Income Tax";

        protected override Dictionary<int, Dictionary<int, double>> GetAllRules()
        {
            return new Dictionary<int, Dictionary<int, double>>
            {
                {
                    2021, new Dictionary<int, double>
                    {
                        { 0, 0 },
                        { 18201, 19 },
                        { 37001, 32.5 },
                        { 87001, 37 },
                        { 180001, 47 }
                    }
                }
            };
        }

    }
}
