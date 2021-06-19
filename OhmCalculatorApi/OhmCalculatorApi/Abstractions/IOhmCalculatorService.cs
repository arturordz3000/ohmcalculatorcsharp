using System;
namespace OhmCalculatorApi.Abstractions
{
    public interface IOhmCalculatorService<TResult> : IDisposable
    {
        TResult Calculate(int color1Id, int color2Id, int colorMultiplierId, int colorToleranceId);
    }
}
