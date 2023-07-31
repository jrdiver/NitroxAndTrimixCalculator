using System;
using System.Diagnostics;

namespace NitroxAndTrimixCalculatorLibrary.Class;

public static class DataVerify
{
    public static double Verify(double value, double low, double high)
    {
        if (value > high)
            return high;
        if (value < low)
            return low;
        return value;
    }

    public static int Verify(int value, int low, int high)
    {
        if (value > high)
            return high;
        if (value < low)
            return low;
        return value;
    }

    public static string PercentToName(double percent)
    {
        return percent switch
        {
            > 20.8 and < 21.1 => "Air",
            > 99.8 => "Oxygen",
            _ => percent + "%"
        };
    }

    public static double VerifyMix(double input)
    {
        double output = Verify(input, 0, 100);
        if (output is < 1 and > 0)
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
}