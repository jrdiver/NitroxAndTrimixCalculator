using System;

namespace NitroxAndTrimixCalculatorLibrary.Object;

public class Unit
{
    public string Name = string.Empty;
    public string PressureName = string.Empty;
    public string DepthName = string.Empty;
    public string VolumeName = string.Empty;
    public double UnitPerBar = 1;
    public double UnitPerMeter = 1;
    public double UnitPerLiter = 1;
    public int RoundPressureTo = 1;
    public int RoundDepthTo = 1;
    public int RoundVolumeTo = 1;

    /// <summary> The Current Depth Converted to Metric </summary>
    public double DepthMeter { get; set; }

    /// <summary> The Current Pressure Converted to Metric </summary>
    public double PressureBar { get; set; }

    /// <summary> The Current Volume Converted to Metric </summary>
    public double VolumeLiter { get; set; }

    /// <summary> Depth in the Currently Selected Unit </summary>
    public double Depth
    {
        get => Math.Round(DepthMeter * UnitPerMeter, RoundDepthTo);
        set => DepthMeter = value / UnitPerMeter;
    }

    /// <summary> Volume in the Currently Selected Unit </summary>
    public double Volume
    {
        get => Math.Round(VolumeLiter * UnitPerLiter, RoundVolumeTo);
        set => VolumeLiter = value / UnitPerLiter;
    }

    /// <summary> Pressure in the currently Selected Unit </summary>
    public double Pressure
    {
        get => Math.Round(PressureBar * UnitPerBar, RoundPressureTo);
        set => PressureBar = value / UnitPerBar;
    }
}