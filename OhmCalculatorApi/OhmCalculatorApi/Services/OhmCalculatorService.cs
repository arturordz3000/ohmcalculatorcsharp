using System;
using System.Collections.Generic;
using System.Linq;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.Services
{
    public class OhmCalculatorService : IOhmCalculatorService<string>
    {
        private readonly IOhmCalculatorUnitOfWork unitOfWork;

        public OhmCalculatorService(IOhmCalculatorUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public string Calculate(int firstValueColorId, int secondValueColorId, int multiplierColorId, int toleranceColorId)
        {
            (var firstColor, var secondColor, var thirdColor, var fourthColor) = ValidateAndGetColors(firstValueColorId, secondValueColorId, multiplierColorId, toleranceColorId);

            string ohms = GetOhmsValue((firstColor.ValueNumber * 10 + secondColor.ValueNumber) * thirdColor.ValueNumber);

            return $"{ohms} Ohms ±{fourthColor.ValueNumber}";
        }

        private (Color, Color, Color, Color) ValidateAndGetColors(int firstValueColorId, int secondValueColorId, int multiplierColorId, int toleranceColorId)
        {
            var colors = unitOfWork.ColorsRepository.Get();
            var firstColor = colors.FirstOrDefault(color => color.Id == firstValueColorId && color.ColorType == ColorType.Value);
            var secondColor = colors.FirstOrDefault(color => color.Id == secondValueColorId && color.ColorType == ColorType.Value);
            var thirdColor = colors.FirstOrDefault(color => color.Id == multiplierColorId && color.ColorType == ColorType.Multiplier);
            var fourthColor = colors.FirstOrDefault(color => color.Id == toleranceColorId && color.ColorType == ColorType.Tolerance);

            if (firstColor is null)
            {
                throw new ArgumentException("Invalid color ID", nameof(firstValueColorId));
            }

            if (secondColor is null)
            {
                throw new ArgumentException("Invalid color ID", nameof(secondValueColorId));
            }

            if (thirdColor is null)
            {
                throw new ArgumentException("Invalid color ID", nameof(multiplierColorId));
            }

            if (fourthColor is null)
            {
                throw new ArgumentException("Invalid color ID", nameof(toleranceColorId));
            }

            return (firstColor, secondColor, thirdColor, fourthColor);
        }

        private string GetOhmsValue(double ohms)
        {
            if (ohms >= 1e9)
            {
                return (ohms / 1e9) + "G";
            }

            if (ohms >= 1e6)
            {
                return (ohms / 1e6) + "M";
            }

            if (ohms >= 1000)
            {
                return (ohms / 1000) + "K";
            }

            return ohms.ToString();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
