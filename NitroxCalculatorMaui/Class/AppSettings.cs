using Newtonsoft.Json;
using NitroxAndTrimixCalculatorLibrary.Class;

namespace NitroxCalculatorMaui.Class;

internal static class AppSettings
{
    public static string SelectedUnit
    {
        get => Preferences.Get("SelectedUnit", "Imperial");
        set => Preferences.Set("SelectedUnit", value);
    }

    public static void DefaultSelectedUnit() => Preferences.Remove("SelectedUnit");

    public static double TankSize
    {
        get => Preferences.Get("TankSize", 2265.35);
        set => Preferences.Set("TankSize", value);
    }

    public static void DefaultTankSize() => Preferences.Remove("TankSize");

    public static double StartPressure
    {
        get => Preferences.Get("StartPressure", 34.4738);
        set => Preferences.Set("StartPressure", value);
    }

    public static void DefaultStartPressure() => Preferences.Remove("StartPressure");

    public static double EndPressure
    {
        get => Preferences.Get("EndPressure", 234.4217);
        set => Preferences.Set("EndPressure", value);
    }

    public static void DefaultEndPressure() => Preferences.Remove("EndPressure");

    public static double MaxOxygenPressure
    {
        get => Preferences.Get("MaxOxygenPressure", 310.2641);
        set => Preferences.Set("MaxOxygenPressure", value);
    }

    public static void DefaultMaxOxygenPressure() => Preferences.Remove("MaxOxygenPressure");

    public static double StartMix
    {
        get => Preferences.Get("StartMix", 32.0);
        set => Preferences.Set("StartMix", DataVerify.VerifyMix(value));
    }

    public static void DefaultStartMix() => Preferences.Remove("StartMix");

    public static double EndMix
    {
        get => Preferences.Get("EndMix", 32.0);
        set => Preferences.Set("EndMix", DataVerify.VerifyMix(value));
    }

    public static void DefaultEndMix() => Preferences.Remove("EndMix");

    public static List<double> P02List
    {
        get => JsonConvert.DeserializeObject<List<double>>(Preferences.Get("P02List", "[1.3, 1.4, 1.5, 1.6]"));
        set => Preferences.Set("P02List", JsonConvert.SerializeObject(value));
    }

    public static void DefaultP02List() => Preferences.Remove("P02List");

    public static void DefaultAll() => Preferences.Clear();
}