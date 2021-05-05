using System.Collections.Generic;

namespace SalaryPackaging.Common
{
    public class SalaryDetail
    {
        public SalaryDetail(double gross, double super, double taxable, double net, double payPacket, PayFrequency payFrequency, IList<Deduction> deductions)
        {
            Gross = gross;
            Super = super;
            Taxable = taxable;
            Net = net;
            PayPacket = payPacket;
            PayFrequency = payFrequency;
            Deductions = deductions;
        }

        public double Gross { get; }

        public double Super { get; }

        public double Taxable { get; }

        public double Net { get; }

        public double PayPacket { get; }

        public PayFrequency PayFrequency { get; }

        public IList<Deduction> Deductions { get; }
    }
}
