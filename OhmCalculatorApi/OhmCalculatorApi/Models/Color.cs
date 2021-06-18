using System;
using System.Collections.Generic;

namespace OhmCalculatorApi.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Rgb { get; set; }
        public string ValueDescription { get; set; }
        public double ValueNumber { get; set; }
        public ICollection<ColorSelectorConfiguration> ColorSelectorConfigurations { get; set; }
    }
}
