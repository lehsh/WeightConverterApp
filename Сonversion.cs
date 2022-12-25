using static System.Formats.Asn1.AsnWriter;
using System.Drawing;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;

namespace WeightConverterJsonAPI
{
    public class MeasureOfWeight
    {
        public MeasureOfWeight(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public double Value { get; set; }
    }
    public class СonversionMeasureOfWeight
    {
        private Dictionary<string, double>? measureCollection;

        public СonversionMeasureOfWeight()
        {
            CollectionUpdate();
        }

        public MeasureOfWeight Gram { get; set; } = new MeasureOfWeight("Gram", 1);
        public MeasureOfWeight Kilogram { get; set; } = new MeasureOfWeight("Kilogram", 0.001);
        public MeasureOfWeight Stone { get; set; } = new MeasureOfWeight("Stone", 0.0001575);
        public MeasureOfWeight Pound { get; set; } = new MeasureOfWeight("Pound", 0.002205);
        public MeasureOfWeight Ounce { get; set; } = new MeasureOfWeight("Ounce", 0.03527);
        public MeasureOfWeight Dram { get; set; } = new MeasureOfWeight("Dram", 0.5644);
        public MeasureOfWeight Grain { get; set; } = new MeasureOfWeight("Grain", 15.43);

        private void CollectionUpdate()
        {
            measureCollection = new Dictionary<string, double>()
            {
                { Gram.Name, Gram.Value },
                { Kilogram.Name, Kilogram.Value },
                { Stone.Name, Stone.Value },
                { Pound.Name, Pound.Value },
                { Ounce.Name, Ounce.Value },
                { Dram.Name, Dram.Value },             
                { Grain.Name, Grain.Value }                
            };
        }

        public void ChangeValue(MeasureOfWeight measure)
        {
            double ratio;
            if (measureCollection != null)
            {
                foreach (var item in measureCollection)
                {
                    if (measure.Name == item.Key)
                    {
                        ratio = item.Value / measure.Value;

                        Gram.Value = Gram.Value / ratio;
                        Kilogram.Value = Kilogram.Value / ratio;
                        Stone.Value = Stone.Value / ratio;
                        Pound.Value = Pound.Value / ratio;
                        Ounce.Value = Ounce.Value / ratio;
                        Dram.Value = Dram.Value / ratio;
                        Grain.Value = Grain.Value / ratio;

                        CollectionUpdate();
                        return;
                    }
                }
            }
        }

        public override string ToString()
        {
            string fullString = "";

            if(measureCollection != null)
            {
                foreach (var measure in measureCollection)
                {
                    fullString += measure.Key + ' ' + measure.Value + '\n';
                }
                return fullString;
            }
            else
            {
                return "Initialization error";
            }            
        }
    }

    public record ErrorMessage(string Error);
}
