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


    public double DepthMeter { get; private set; }
    public double PressureBar { get; private set; }
    public double VolumeLiter { get; private set; }

    public double GetDepth()
    {
        return Math.Round(DepthMeter * UnitPerMeter, RoundDepthTo);
    }

    public void SetDepth(double depth)
    {
        DepthMeter = depth / UnitPerMeter;
    }

    public void SetDepthInMeters(double depth)
    {
        DepthMeter = depth;
    }

    public double GetPressure()
    {
        return Math.Round(PressureBar * UnitPerBar, RoundPressureTo);
    }

    public void SetPressure(double pressure)
    {
        PressureBar = pressure / UnitPerBar;
    }

    public void SetPressureInBars(double pressure)
    {
        PressureBar = pressure;
    }

    public void SetVolumeInLiters(double volume)
    {
        VolumeLiter = volume;
    }

    public void SetVolume(double volume)
    {
        VolumeLiter = volume / UnitPerLiter;
    }

    public double GetVolume()
    {
        return Math.Round(VolumeLiter * UnitPerLiter, RoundVolumeTo);

    }
}