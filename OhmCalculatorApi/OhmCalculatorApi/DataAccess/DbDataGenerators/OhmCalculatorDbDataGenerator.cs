using System;
using System.Collections.Generic;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.Exceptions;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.DataAccess.DbDataGenerators
{
    public class OhmCalculatorDbDataGenerator : IDbDataGenerator
    {
        private readonly IOhmCalculatorUnitOfWork unitOfWork;

        public OhmCalculatorDbDataGenerator(IOhmCalculatorUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Generate()
        {
            if (unitOfWork.Database != null)
            {
                unitOfWork.Database.EnsureDeleted();
                unitOfWork.Database.EnsureCreated();
            }

            var bandColors = CreateBandColors();
            var multiplierColors = CreateMultiplierColors();
            var toleranceColors = CreateToleranceColors();

            var allColors = new List<Color>();
            allColors.AddRange(bandColors);
            allColors.AddRange(multiplierColors);
            allColors.AddRange(toleranceColors);

            foreach (var color in allColors)
            {
                unitOfWork.ColorsRepository.Insert(color);
            }

            var colorSelectorConfigurations = new List<ColorSelectorConfiguration>();
            colorSelectorConfigurations.Add(new ColorSelectorConfiguration { Id = 1, Name = "First Band", Colors = bandColors });
            colorSelectorConfigurations.Add(new ColorSelectorConfiguration { Id = 2, Name = "Second Band", Colors = bandColors });
            colorSelectorConfigurations.Add(new ColorSelectorConfiguration { Id = 3, Name = "Multiplier", Colors = multiplierColors });
            colorSelectorConfigurations.Add(new ColorSelectorConfiguration { Id = 4, Name = "Tolerance", Colors = toleranceColors });

            foreach (var colorSelectorConfiguration in colorSelectorConfigurations)
            {
                unitOfWork.ColorSelectorConfigurationsRepository.Insert(colorSelectorConfiguration);
            }

            var resistorDefaults = new List<ResistorDefault>();
            resistorDefaults.Add(new ResistorDefault { Id = 1, Position = "20px", Color = allColors[0] });
            resistorDefaults.Add(new ResistorDefault { Id = 2, Position = "40px", Color = allColors[0] });
            resistorDefaults.Add(new ResistorDefault { Id = 3, Position = "60px", Color = allColors[10] });
            resistorDefaults.Add(new ResistorDefault { Id = 4, Position = "180px", Color = allColors[26] });

            foreach (var resistorDefault in resistorDefaults)
            {
                unitOfWork.ResistorDefaultsRepository.Insert(resistorDefault);
            }

            unitOfWork.Save();
        }

        private IList<Color> CreateBandColors()
        {
            IList<Color> colors = new List<Color>();
            var type = ColorType.Value;

            colors.Add(new Color { Id = 1, Rgb = "rgb(0, 0, 0)", ValueDescription = "0", ValueNumber = 0, ColorType = type });
            colors.Add(new Color { Id = 2, Rgb = "rgb(153, 117, 82)", ValueDescription = "1", ValueNumber = 1, ColorType = type });
            colors.Add(new Color { Id = 3, Rgb = "rgb(255, 57, 57)", ValueDescription = "2", ValueNumber = 2, ColorType = type });
            colors.Add(new Color { Id = 4, Rgb = "rgb(255, 165, 74)", ValueDescription = "3", ValueNumber = 3, ColorType = type });
            colors.Add(new Color { Id = 5, Rgb = "rgb(255, 255, 122)", ValueDescription = "4", ValueNumber = 4, ColorType = type });
            colors.Add(new Color { Id = 6, Rgb = "rgb(137, 255, 137)", ValueDescription = "5", ValueNumber = 5, ColorType = type });
            colors.Add(new Color { Id = 7, Rgb = "rgb(72, 136, 242)", ValueDescription = "6", ValueNumber = 6, ColorType = type });
            colors.Add(new Color { Id = 8, Rgb = "rgb(240, 144, 246)", ValueDescription = "7", ValueNumber = 7, ColorType = type });
            colors.Add(new Color { Id = 9, Rgb = "rgb(128, 128, 128)", ValueDescription = "8", ValueNumber = 8, ColorType = type });
            colors.Add(new Color { Id = 10, Rgb = "rgb(255, 255, 255)", ValueDescription = "9", ValueNumber = 9, ColorType = type });

            return colors;
        }

        private IList<Color> CreateMultiplierColors()
        {
            IList<Color> colors = new List<Color>();
            var type = ColorType.Multiplier;

            colors.Add(new Color { Id = 11, Rgb = "rgb(0, 0, 0)", ValueDescription = "1Ω", ValueNumber = 1, ColorType = type });
            colors.Add(new Color { Id = 12, Rgb = "rgb(153, 117, 82)", ValueDescription = "10Ω", ValueNumber = 10, ColorType = type });
            colors.Add(new Color { Id = 13, Rgb = "rgb(255, 57, 57)", ValueDescription = "100Ω", ValueNumber = 100, ColorType = type });
            colors.Add(new Color { Id = 14, Rgb = "rgb(255, 165, 74)", ValueDescription = "1KΩ", ValueNumber = 1000, ColorType = type });
            colors.Add(new Color { Id = 15, Rgb = "rgb(255, 255, 122)", ValueDescription = "10KΩ", ValueNumber = 10000, ColorType = type });
            colors.Add(new Color { Id = 16, Rgb = "rgb(137, 255, 137)", ValueDescription = "100KΩ", ValueNumber = 100000, ColorType = type });
            colors.Add(new Color { Id = 17, Rgb = "rgb(72, 136, 242)", ValueDescription = "1MΩ", ValueNumber = 1000000, ColorType = type });
            colors.Add(new Color { Id = 18, Rgb = "rgb(240, 144, 246)", ValueDescription = "10MΩ", ValueNumber = 10000000, ColorType = type });
            colors.Add(new Color { Id = 19, Rgb = "rgb(205, 153, 51)", ValueDescription = "0.1", ValueNumber = 0.1, ColorType = type });
            colors.Add(new Color { Id = 20, Rgb = "rgb(204, 204, 204)", ValueDescription = "0.01", ValueNumber = 0.01, ColorType = type });

            return colors;
        }

        private IList<Color> CreateToleranceColors()
        {
            IList<Color> colors = new List<Color>();
            var type = ColorType.Tolerance;

            colors.Add(new Color { Id = 21, Rgb = "rgb(153, 117, 82)", ValueDescription = "±1%", ValueNumber = 1, ColorType = type });
            colors.Add(new Color { Id = 22, Rgb = "rgb(255, 57, 57)", ValueDescription = "±2%", ValueNumber = 2, ColorType = type });
            colors.Add(new Color { Id = 23, Rgb = "rgb(137, 255, 137)", ValueDescription = "±0.5%", ValueNumber = 0.5, ColorType = type });
            colors.Add(new Color { Id = 24, Rgb = "rgb(72, 136, 242)", ValueDescription = "±0.25%", ValueNumber = 0.25, ColorType = type });
            colors.Add(new Color { Id = 25, Rgb = "rgb(240, 144, 246)", ValueDescription = "±0.10%", ValueNumber = 0.1, ColorType = type });
            colors.Add(new Color { Id = 26, Rgb = "rgb(128, 128, 128)", ValueDescription = "±0.05%", ValueNumber = 0.05, ColorType = type });
            colors.Add(new Color { Id = 27, Rgb = "rgb(205, 153, 51)", ValueDescription = "±5%", ValueNumber = 5, ColorType = type });
            colors.Add(new Color { Id = 28, Rgb = "rgb(204, 204, 204)", ValueDescription = "±10%", ValueNumber = 10, ColorType = type });

            return colors;
        }
    }
}
