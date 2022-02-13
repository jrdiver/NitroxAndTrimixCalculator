namespace NitroxAndTrimixCalculatorLibrary.Class;

internal static class DataVerify
{
    internal static double VerifyDouble(double value, double low, double high)
    {
        if (value > high)
        {
            return high;
        }
        if (value < low)
        {
            return low;
        }

        return value;
    }

    internal static int VerifyInt(int value, int low, int high)
    {
        if (value > high)
        {
            return high;
        }
        if (value < low)
        {
            return low;
        }

        return value;
    }
}