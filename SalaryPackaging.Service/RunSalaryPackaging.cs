using System;
using SalaryPackaging.Common;
using SalaryPackaging.Service.Model;
using SalaryPackaging.Core.Interface;
using SalaryPackaging.Service.Contract;
using SalaryPackaging.Business.Contract;

namespace SalaryPackaging.Service
{
    public class RunSalaryPackaging : IDemoSalaryPackaging
    {
        private readonly IProvideSalaryBreakdown _provideSalaryBreakdown;
        private readonly IFormatSalaryBreakdown _formatSalaryBreakdown;
        private readonly IExceptionHandler _exceptionHandler;

        public RunSalaryPackaging(IProvideSalaryBreakdown provideSalaryBreakdown, IFormatSalaryBreakdown formatSalaryBreakdown, IExceptionHandler exceptionHandler)
        {
            _provideSalaryBreakdown = provideSalaryBreakdown;
            _formatSalaryBreakdown = formatSalaryBreakdown;
            _exceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// entry method which does basic orchestration
        /// 1. Gets user inputs
        /// 2. Gets salary packaging information in raw form
        /// 3. Using correct formatter to display breakdown of salary packaging
        /// </summary>
        public void Show()
        {
            try
            {
                PayInputModel payInput = GetPayInput();

                Console.WriteLine();
                Console.WriteLine("Calculating salary details..");
                Console.WriteLine();

                SalaryDetail salaryDetail = GetSalaryPackingDetail(payInput);
                PrintSalaryBreakdown(salaryDetail);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Write(ex);
                Console.WriteLine();
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Error details: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to end..."); ;
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Re-try till we get valid user inputs
        /// </summary>
        /// <returns></returns>
        protected PayInputModel GetPayInput()
        {
            double amount;
            do
            {
                Console.Write("Enter your salary package amount: ");
                var amountString = Console.ReadLine();
                double.TryParse(amountString, out amount);
            }
            while (amount <= 0);

            PayFrequency frequency;
            do
            {
                Console.Write("Enter your pay frequency (W for weekly, F for fortnightly,  M for monthly): ");
                var frequencyChar = Console.ReadKey().KeyChar;
                Enum.TryParse<PayFrequency>(frequencyChar.ToString().ToUpper(), out frequency);
            }
            while (frequency == PayFrequency.N);

            Console.WriteLine();
            return new PayInputModel(amount, frequency);
        }

        /// <summary>
        /// Call business component or service to get packaging details in row material
        /// </summary>
        /// <param name="salaryPackagingInputModel"></param>
        /// <returns></returns>
        private SalaryDetail GetSalaryPackingDetail(PayInputModel salaryPackagingInputModel)
        {
            return _provideSalaryBreakdown.Get(salaryPackagingInputModel.GrossPay, salaryPackagingInputModel.PayFrequency);
        }

        /// <summary>
        /// This is usually rest layer or ui related stuff that how to format or display
        /// </summary>
        /// <param name="salaryDetail"></param>
        private void PrintSalaryBreakdown(SalaryDetail salaryDetail)
        {
            _formatSalaryBreakdown.Print(salaryDetail);
        }
    }
}
