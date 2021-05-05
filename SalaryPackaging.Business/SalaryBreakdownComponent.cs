using System;
using System.Linq;
using SalaryPackaging.Common;
using Microsoft.Extensions.Options;
using SalaryPackaging.Core.Interface;
using SalaryPackaging.Business.Contract;

namespace SalaryPackaging.Business
{
    public class SalaryBreakdownComponent : IProvideSalaryBreakdown
    {
        private readonly double _superRate;
        private readonly IDeductionCalculator _deductionCalc;
        private readonly ITracer _tracer;

        public SalaryBreakdownComponent(IOptions<AppSetting> settings, IDeductionCalculator deductionCalc, ITracer tracer)
        {
            _superRate = double.Parse(settings.Value["SuperRate"]);
            _deductionCalc = deductionCalc;
            _tracer = tracer;
        }

        public SalaryDetail Get(double grossAmount, PayFrequency payFrequency)
        {
            // TODO - We could have guards to protect inputs coming in business components i.e. extending from fluent validation
            // ArgumentGuard.Ensure(() => payAmount).IsGreaterThan(0);
            // ArgumentGuard.Ensure(() => payFrequency).IsNotEqualTo(PayFrequency.N);           

            var super = CalcSuper(grossAmount);
            _tracer.Log($"Super is {super}");

            var taxable = Math.Round(grossAmount - super, 2, MidpointRounding.ToEven);
            _tracer.Log($"Taxable income is {taxable}");

            var deductions = _deductionCalc.GetDeductions(taxable);
            _tracer.Log("Deductions are done!");

            var net = taxable - deductions.Sum(x => x.Amount);
            _tracer.Log($"Net income is {net}");

            var packet = CalcPayPacket(net, payFrequency);
            _tracer.Log($"Packet is {packet}");

            return new SalaryDetail(grossAmount, super, taxable, net, packet, payFrequency, deductions);
        }

        private double CalcSuper(double gross)
        {
            return Math.Round((gross * _superRate) / (100 + _superRate), 2, MidpointRounding.AwayFromZero);
        }

        private double CalcPayPacket(double net, PayFrequency payFrequency)
        {
            switch (payFrequency)
            {
                case PayFrequency.M:
                    return Math.Round(net / 12, 2, MidpointRounding.AwayFromZero);

                case PayFrequency.F:
                    return Math.Round(net / 26, 2, MidpointRounding.AwayFromZero);

                case PayFrequency.W:
                    return Math.Round(net / 52, 2, MidpointRounding.AwayFromZero);

                default:
                    throw new ArgumentOutOfRangeException(nameof(payFrequency));
            }
        }
    }
}
