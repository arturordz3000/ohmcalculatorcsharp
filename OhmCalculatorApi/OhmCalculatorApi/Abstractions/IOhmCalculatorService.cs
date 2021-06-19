using System;
namespace OhmCalculatorApi.Abstractions
{
    public interface IOhmCalculatorService<TResult> : IDisposable
    {
        TResult Calculate(int firstId, int secondId, int multiplierId, int toleranceId);
    }
}
