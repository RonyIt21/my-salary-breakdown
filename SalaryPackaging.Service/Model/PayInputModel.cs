using SalaryPackaging.Common;

namespace SalaryPackaging.Service.Model
{
    public class PayInputModel
    {
        public PayInputModel(double grossPay, PayFrequency payFrequency)
        {
            // TODO - We could have guards to protect inputs coming in business components i.e. extending from fluent validation
            // ArgumentGuard.Ensure(() => grossPay).IsGreaterThan(0);
            // ArgumentGuard.Ensure(() => payFrequency).IsNotEqualTo(PayFrequency.N);           

            GrossPay = grossPay;
            PayFrequency = payFrequency;
        }

        public double GrossPay { get; }

        public PayFrequency PayFrequency { get; }
    }
}
