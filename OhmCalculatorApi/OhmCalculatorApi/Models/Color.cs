using System;
using System.Collections.Generic;

namespace OhmCalculatorApi.Models
{
    public enum ColorType
    {
        Value,
        Multiplier,
        Tolerance
    }

    public class Color
    {
        public int Id { get; set; }
        public string Rgb { get; set; }
        public string ValueDescription { get; set; }
        public double ValueNumber { get; set; }
        public ColorType ColorType { get; set; }
    }
}
