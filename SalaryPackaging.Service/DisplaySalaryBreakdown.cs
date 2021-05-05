using System;
using SalaryPackaging.Common;
using SalaryPackaging.Service.Contract;

namespace SalaryPackaging.Service
{
    public class DisplaySalaryBreakdown : IFormatSalaryBreakdown
    {
        public void Print(SalaryDetail salaryDetail)
        {
            Console.WriteLine($"Gross package: {salaryDetail.Gross.ToString("C")}");
            Console.WriteLine($"Superannuation: {salaryDetail.Super.ToString("C")}");
            Console.WriteLine();

            Console.WriteLine($"Taxable income: {salaryDetail.Taxable.ToString("C")}");
            Console.WriteLine();
            
            Console.WriteLine("Deductions");
            foreach (var deduction in salaryDetail.Deductions)
                Console.WriteLine($"{deduction.DisplayName}: {deduction.Amount.ToString("C")}");
            Console.WriteLine();
            
            Console.WriteLine($"Net income: {salaryDetail.Net}");
            Console.WriteLine($"Pay packet: {salaryDetail.PayPacket.ToString("C")} {GetFrequencyText(salaryDetail.PayFrequency)}");
        }

        private string GetFrequencyText(PayFrequency payFrequency)
        {
            switch (payFrequency)
            {
                case PayFrequency.W:
                    return "per week";

                case PayFrequency.F:
                    return "per fortnight";

                case PayFrequency.M:
                    return "per month";

                default:
                    throw new ArgumentOutOfRangeException(nameof(payFrequency));
            }
        }
    }
}
