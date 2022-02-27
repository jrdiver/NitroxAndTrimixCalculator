using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorLibrary;

public static class UnitConversion
{
    public static Unit ConvertUnit(Unit startUnit, Unit endUnit)
    {
        endUnit.PressureBar = startUnit.PressureBar;
        endUnit.DepthMeter = startUnit.DepthMeter;
        endUnit.VolumeLiter = startUnit.VolumeLiter;

        return endUnit;
    }
}