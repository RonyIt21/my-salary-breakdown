using System;
using System.Linq;
using System.Collections.Generic;

namespace SalaryPackaging.Business.Deduction
{
    internal abstract class DeductionRuleBase : IDeductionRule
    {
        protected int _taxYear;
        protected readonly Dictionary<int, double> _currRule;

        internal DeductionRuleBase(int taxYear)
        {
            // TODO - We could have guards to protect inputs coming in business components i.e. extending from fluent validation
            // ArgumentGuard.Ensure(() => taxYear).IsGreaterThan(1900);

            _taxYear = taxYear;
            _currRule = GetAllRules()[taxYear];
        }

        /// <summary>
        /// This can be anywhere - file, store, database etc
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<int, Dictionary<int, double>> GetAllRules();

        protected abstract string DisplayName { get; }


        /// <summary>
        /// This can be overriden in edged case scenario/s
        /// </summary>
        /// <param name="taxableIncome"></param>
        /// <returns></returns>
        public virtual Common.Deduction Calc(double taxableIncome)
        {
            var amount = GetDeductAmount(taxableIncome);
            return new Common.Deduction(DisplayName, amount);
        }

        private double GetDeductAmount(double taxableIncome)
        {
            double amount = 0;
            var keys = _currRule.Keys.ToList();
            keys.Sort();

            for (int i = 0; i < keys.Count; i++)
            {
                var rate = _currRule.ElementAtOrDefault(i).Value;

                if (rate > 0)
                {
                    double diff = 0;
                    if (i + 1 == keys.Count)
                        diff = taxableIncome - keys[i];
                    else
                        diff = Math.Min(taxableIncome - keys[i], keys[i + 1] - keys[i]);

                    if (diff > 0)
                        amount += diff * rate / 100;
                }
            }

            return Math.Ceiling(amount);
        }

    }
}
