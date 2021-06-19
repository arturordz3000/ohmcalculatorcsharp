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

        public string Calculate(int firstId, int secondId, int multiplierId, int toleranceId)
        {
            (var firstColor, var secondColor, var thirdColor, var fourthColor) = ValidateAndGetColors(firstId, secondId, multiplierId, toleranceId);

            string ohms = GetOhmsValue((firstColor.ValueNumber * 10 + secondColor.ValueNumber) * thirdColor.ValueNumber);

            return $"{ohms} Ohms ±{fourthColor.ValueNumber}%";
        }

        private (Color, Color, Color, Color) ValidateAndGetColors(int firstId, int secondId, int multiplierId, int toleranceId)
        {
            var colors = unitOfWork.ColorsRepository.Get();
            var firstColor = colors.FirstOrDefault(color => color.Id == firstId && color.ColorType == ColorType.Value);
            var secondColor = colors.FirstOrDefault(color => color.Id == secondId && color.ColorType == ColorType.Value);
            var thirdColor = colors.FirstOrDefault(color => color.Id == multiplierId && color.ColorType == ColorType.Multiplier);
            var fourthColor = colors.FirstOrDefault(color => color.Id == toleranceId && color.ColorType == ColorType.Tolerance);

            if (firstColor is null)
            {
                throw new ArgumentException("Invalid color ID", nameof(firstId));
            }

            if (secondColor is null)
            {
                throw new ArgumentException("Invalid color ID", nameof(secondId));
            }

            if (thirdColor is null)
            {
                throw new ArgumentException("Invalid color ID", nameof(multiplierId));
            }

            if (fourthColor is null)
            {
                throw new ArgumentException("Invalid color ID", nameof(toleranceId));
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
