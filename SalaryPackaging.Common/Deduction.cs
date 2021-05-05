namespace SalaryPackaging.Common
{
    public class Deduction
    {
        public Deduction(string displayName, double amount)
        {
            DisplayName = displayName;
            Amount = amount;
        }

        public string DisplayName { get; }

        public double Amount { get; }
    }
}
