using System.Collections.Generic;
using System.Text.Json;
using NitroxAndTrimixCalculatorLibrary.Class;
using NitroxCalculatorUI.Services;

namespace NitroxCalculatorBlazor.Services;

public class WebAppSettings : IAppSettings
{
    private readonly Dictionary<string, string> _store = new();

    private string Get(string key, string defaultValue)
    {
        return _store.TryGetValue(key, out var val) ? val : defaultValue;
    }

    private void Set(string key, string value)
    {
        _store[key] = value;
    }

    private double GetDouble(string key, double defaultValue)
    {
        if (_store.TryGetValue(key, out var val) && double.TryParse(val, out double result))
            return result;
        return defaultValue;
    }

    private void SetDouble(string key, double value)
    {
        _store[key] = value.ToString();
    }

    private void Remove(string key)
    {
        _store.Remove(key);
    }

    public void DefaultAll()
    {
        string currentColorMode = ColorMode;
        _store.Clear();
        ColorMode = currentColorMode;
    }

    #region AppWideSettings
    public string SelectedUnit
    {
        get => Get("SelectedUnit", "Imperial");
        set => Set("SelectedUnit", value);
    }

    public void DefaultSelectedUnit() => Remove("SelectedUnit");

    public string ColorMode
    {
        get => Get("ColorMode", "light").ToLower();
        set => Set("ColorMode", value.ToLower());
    }

    public void DefaultColorMode() => Remove("ColorMode");
    #endregion

    #region NitroxCalculator
    public double StartPressure
    {
        get => GetDouble("StartPressure", 34.4738);
        set => SetDouble("StartPressure", value);
    }

    public void DefaultStartPressure() => Remove("StartPressure");

    public double EndPressure
    {
        get => GetDouble("EndPressure", 234.4217);
        set => SetDouble("EndPressure", value);
    }

    public void DefaultEndPressure() => Remove("EndPressure");

    public double MaxOxygenPressure
    {
        get => GetDouble("MaxOxygenPressure", 310.2641);
        set => SetDouble("MaxOxygenPressure", value);
    }

    public void DefaultMaxOxygenPressure() => Remove("MaxOxygenPressure");

    public double StartMix
    {
        get => GetDouble("StartMix", 32.0);
        set => SetDouble("StartMix", DataVerify.VerifyMix(value));
    }

    public void DefaultStartMix() => Remove("StartMix");

    public double EndMix
    {
        get => GetDouble("EndMix", 32.0);
        set => SetDouble("EndMix", DataVerify.VerifyMix(value));
    }

    public void DefaultEndMix() => Remove("EndMix");

    public List<double> P02List
    {
        get
        {
            var json = Get("P02List", "[1.3, 1.4, 1.5, 1.6]");
            return JsonSerializer.Deserialize<List<double>>(json) ?? new List<double>();
        }
        set => Set("P02List", JsonSerializer.Serialize(value));
    }

    public void DefaultP02List() => Remove("P02List");
    #endregion

    #region PressureSettings
    public double EqualizationTankSize
    {
        get => GetDouble("EqualizationTankSize", 2265.35);
        set => SetDouble("EqualizationTankSize", value);
    }
    public void DefaultTankSize() => Remove("EqualizationTankSize");

    public double EqualizationTankFullPressure
    {
        get => GetDouble("EqualizationTankFullPressure", 206.8427);
        set => SetDouble("EqualizationTankFullPressure", value);
    }
    public void DefaultEqualizationTankFullPressure() => Remove("EqualizationTankFullPressure");

    public double EqualizationTank1Pressure
    {
        get => GetDouble("EqualizationTank1Pressure", 34.4738);
        set => SetDouble("EqualizationTank1Pressure", value);
    }
    public void DefaultEqualizationTank1Pressure() => Remove("EqualizationTank1Pressure");

    public double EqualizationTank2Pressure
    {
        get => GetDouble("EqualizationTank2Pressure", 172.3689);
        set => SetDouble("EqualizationTank2Pressure", value);
    }

    public void DefaultEqualizationTank2Pressure() => Remove("EqualizationTank2Pressure");
    #endregion
}
