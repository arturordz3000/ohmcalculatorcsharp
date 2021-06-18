using System;
using System.Collections.Generic;

namespace OhmCalculatorApi.Models
{
    public class ColorSelectorConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Color> Colors { get; set; }
    }
}
