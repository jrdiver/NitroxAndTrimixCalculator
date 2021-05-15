using System.Collections.Generic;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorLibrary.Class
{
    public static class LoadUnits
    {
        public static List<Unit> LoadAllUnits()
        {
            List<Unit> units = new List<Unit>
            {
                new Unit
                {
                    Name = "Imperial",
                    PressureName = "PSI",
                    DepthName = "Feet",
                    UnitPerBar = 14.5037738,
                    UnitPerMeter = 3.2808
                },
                new Unit
                {
                    Name = "Metric",
                    PressureName = "Bar",
                    DepthName = "Meter",
                    UnitPerBar = 1,
                    UnitPerMeter = 1
                }
            };

            return units;
        }
    }
}
