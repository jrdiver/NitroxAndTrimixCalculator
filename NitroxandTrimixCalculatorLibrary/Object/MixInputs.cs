using System;
using System.Diagnostics;
using NitroxAndTrimixCalculatorLibrary.Class;

namespace NitroxAndTrimixCalculatorLibrary.Object;

public class MixInputs
{
    private double startPressure = 500;
    public double StartPressure
    {
        get => DataVerify.Verify(startPressure, MinimumPressure, MaxPressure);
        set => startPressure = DataVerify.Verify(value, MinimumPressure, MaxPressure);
    }

    private double endPressure = 3400;
    public double EndPressure
    {
        get => DataVerify.Verify(endPressure, MinimumPressure, MaxPressure);
        set => endPressure = DataVerify.Verify(value, MinimumPressure, MaxPressure);
    }

    private double maxOxygenPressure = 4500;
    public double MaxOxygenPressure
    {
        get => DataVerify.Verify(maxOxygenPressure, MinimumPressure, MaxPressure);
        set => maxOxygenPressure = DataVerify.Verify(value, MinimumPressure, MaxPressure);
    }

    private double startMix = 32;
    public double StartMix
    {
        get => startMix;
        set => startMix = VerifyMix(value);
    }

    private double endMix = 32;
    public double EndMix
    {
        get => endMix;
        set => endMix = VerifyMix(value);
    }

    private double topOffMix = 20.9;
    public double TopOffMix
    {
        get => topOffMix;
        set => topOffMix = VerifyMix(value);
    }

    private double maxPressure = 10000000000000000000;
    public double MaxPressure
    {
        get => maxPressure;
        set => maxPressure = Math.Abs(value);
    }

    private double minimumPressure;
    public double MinimumPressure
    {
        get => minimumPressure;
        set => minimumPressure = Math.Abs(value);
    }

    public bool EnableMaxOxygenPressure = false;

    /// <summary> Returns the pressure difference between the start and end pressures </summary>
    public double PressureDifference => Math.Abs(EndPressure - StartPressure);

    /// <summary> Returns if the End Pressure Larger then the Start Pressure </summary>
    public bool EndPressureLarger => StartPressure <= EndPressure;

    public double StartMixDecimal => StartMix / 100;

    public double EndMixDecimal => EndMix / 100;

    public double TopOffMixDecimal => TopOffMix / 100;

    public string GetTopOffGasName => PercentToName(TopOffMix);

    public string GetStartGasName => PercentToName(StartMix);

    public string GetEndGasName => PercentToName(EndMix);

    #region PrivateMethoods
    private string PercentToName(double percent)
    {
        return percent switch
        {
            > 20.8 and < 21.1 => "Air",
            > 99.8 => "Oxygen",
            _ => percent + "%"
        };
    }

    private double VerifyMix(double input)
    {
        double output = DataVerify.Verify(input, 0, 100);
        if (output < 1 && output > 0)
        {
            output *= 100;
            input *= 100;
        }

        if (Math.Abs(input - output) > .01)
        {
            Debug.WriteLine("Invalid Mix Input");
        }
        return output;
    }
    #endregion
}