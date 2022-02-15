using System.Collections.Generic;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorLibrary.Class;

public static class LoadUnits
{
    public static List<Unit> LoadAllUnits()
    {
        List<Unit> units = new ()
        {
            new Unit
            {
                Name = "Imperial",
                PressureName = "PSI",
                DepthName = "Feet",
                VolumeName = "Cubic Feet",
                UnitPerBar = 14.5037738,
                UnitPerMeter = 3.2808,
                UnitPerLiter = 0.0353147,
                RoundPressureTo = 0,
                
            },
            new Unit
            {
                Name = "Metric",
                PressureName = "Bar",
                DepthName = "Meter",
                VolumeName = "Liter",
                UnitPerBar = 1,
                UnitPerMeter = 1,
                UnitPerLiter = 1,
            }
        };

        return units;
    }
}