using Newtonsoft.Json;
using NitroxAndTrimixCalculatorLibrary.Class;

namespace NitroxCalculatorMaui.Class;

internal static class AppSettings
{
    #region AppWideSettings
    public static string SelectedUnit
    {
        get => Preferences.Get("SelectedUnit", "Imperial");
        set => Preferences.Set("SelectedUnit", value);
    }

    public static void DefaultSelectedUnit() => Preferences.Remove("SelectedUnit");

    public static void DefaultAll() => Preferences.Clear();
    #endregion

    #region NitroxCalculator
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
    #endregion

    #region PressureSettings
    public static double EqualizationTankSize
    {
        get => Preferences.Get("EqualizationTankSize", 2265.35);
        set => Preferences.Set("EqualizationTankSize", value);
    }
    public static void DefaultTankSize() => Preferences.Remove("EqualizationTankSize");

    public static double EqualizationTankFullPressure
    {
        get => Preferences.Get("EqualizationTankFullPressure", 206.8427);
        set => Preferences.Set("EqualizationTankFullPressure", value);
    }
    public static void DefaultEqualizationTankFullPressure() => Preferences.Remove("EqualizationTankFullPressure");

    public static double EqualizationTank1Pressure
    {
        get => Preferences.Get("EqualizationTank1Pressure", 34.4738);
        set => Preferences.Set("EqualizationTank1Pressure", value);
    }
    public static void DefaultEqualizationTank1Pressure() => Preferences.Remove("EqualizationTank1Pressure");

    public static double EqualizationTank2Pressure
    {
        get => Preferences.Get("EqualizationTank2Pressure", 172.3689);
        set => Preferences.Set("EqualizationTank2Pressure", value);
    }

    public static void DefaultEqualizationTank2Pressure() => Preferences.Remove("EqualizationTank2Pressure");
    #endregion
}