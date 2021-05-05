using System;

namespace SalaryPackaging.Core.Interface
{
    public interface IExceptionHandler
    {
        void Write(Exception ex);
    }
}
