namespace SalaryPackaging.Core.Interface
{
    /// <summary>
    /// We could implement tracer to write messages to trace file
    /// </summary>
    public interface ITracer
    {
        void Log(string message);
    }
}
