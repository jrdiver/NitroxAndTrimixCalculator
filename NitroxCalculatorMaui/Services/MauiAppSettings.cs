using Newtonsoft.Json;
using NitroxAndTrimixCalculatorLibrary.Class;
using NitroxCalculatorUI.Services;

namespace NitroxCalculatorMaui.Services;

public class MauiAppSettings : IAppSettings
{
    public void DefaultAll()
    {
        string currentColorMode = ColorMode;
        Preferences.Clear();
        ColorMode = currentColorMode;
    }

    #region AppWideSettings
    public string SelectedUnit
    {
        get => Preferences.Get("SelectedUnit", "Imperial");
        set => Preferences.Set("SelectedUnit", value);
    }

    public void DefaultSelectedUnit() => Preferences.Remove("SelectedUnit");

    public string ColorMode
    {
        get => Preferences.Get("ColorMode", "light").ToLower();
        set => Preferences.Set("ColorMode", value.ToLower());
    }

    public void DefaultColorMode() => Preferences.Remove("ColorMode");
    #endregion

    #region NitroxCalculator
    public double StartPressure
    {
        get => Preferences.Get("StartPressure", 34.4738);
        set => Preferences.Set("StartPressure", value);
    }

    public void DefaultStartPressure() => Preferences.Remove("StartPressure");

    public double EndPressure
    {
        get => Preferences.Get("EndPressure", 234.4217);
        set => Preferences.Set("EndPressure", value);
    }

    public void DefaultEndPressure() => Preferences.Remove("EndPressure");

    public double MaxOxygenPressure
    {
        get => Preferences.Get("MaxOxygenPressure", 310.2641);
        set => Preferences.Set("MaxOxygenPressure", value);
    }

    public void DefaultMaxOxygenPressure() => Preferences.Remove("MaxOxygenPressure");

    public double StartMix
    {
        get => Preferences.Get("StartMix", 32.0);
        set => Preferences.Set("StartMix", DataVerify.VerifyMix(value));
    }

    public void DefaultStartMix() => Preferences.Remove("StartMix");

    public double EndMix
    {
        get => Preferences.Get("EndMix", 32.0);
        set => Preferences.Set("EndMix", DataVerify.VerifyMix(value));
    }

    public void DefaultEndMix() => Preferences.Remove("EndMix");

    public List<double> P02List
    {
        get => JsonConvert.DeserializeObject<List<double>>(Preferences.Get("P02List", "[1.3, 1.4, 1.5, 1.6]")) ?? new List<double>();
        set => Preferences.Set("P02List", JsonConvert.SerializeObject(value));
    }

    public void DefaultP02List() => Preferences.Remove("P02List");
    #endregion

    #region PressureSettings
    public double EqualizationTankSize
    {
        get => Preferences.Get("EqualizationTankSize", 2265.35);
        set => Preferences.Set("EqualizationTankSize", value);
    }
    public void DefaultTankSize() => Preferences.Remove("EqualizationTankSize");

    public double EqualizationTankFullPressure
    {
        get => Preferences.Get("EqualizationTankFullPressure", 206.8427);
        set => Preferences.Set("EqualizationTankFullPressure", value);
    }
    public void DefaultEqualizationTankFullPressure() => Preferences.Remove("EqualizationTankFullPressure");

    public double EqualizationTank1Pressure
    {
        get => Preferences.Get("EqualizationTank1Pressure", 34.4738);
        set => Preferences.Set("EqualizationTank1Pressure", value);
    }
    public void DefaultEqualizationTank1Pressure() => Preferences.Remove("EqualizationTank1Pressure");

    public double EqualizationTank2Pressure
    {
        get => Preferences.Get("EqualizationTank2Pressure", 172.3689);
        set => Preferences.Set("EqualizationTank2Pressure", value);
    }

    public void DefaultEqualizationTank2Pressure() => Preferences.Remove("EqualizationTank2Pressure");
    #endregion
}
