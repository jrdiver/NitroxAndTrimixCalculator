using NitroxAndTrimixCalculatorLibrary;
using NitroxCalculatorMaui.Class;

namespace NitroxCalculatorMaui.Pages;

public partial class EditSettings
{
    private readonly NitroxMixCalculator calculator = new();

    private string SelectedUnit
    {
        get => AppSettings.SelectedUnit;
        set
        {
            AppSettings.SelectedUnit = value;
            calculator.LoadUnit(AppSettings.SelectedUnit);
        }
    }

    private string NitroxStartPressure
    {
        get
        {
            calculator.SelectedUnit.PressureBar = AppSettings.StartPressure;
            return calculator.SelectedUnit.Pressure.ToString();
        }
        set
        {
            double.TryParse(value, out double parsed);
            calculator.SelectedUnit.Pressure = parsed;
            AppSettings.StartPressure = calculator.SelectedUnit.PressureBar;
        }
    }

    private string NitroxEndPressure
    {
        get
        {
            calculator.SelectedUnit.PressureBar = AppSettings.EndPressure;
            return calculator.SelectedUnit.Pressure.ToString();
        }
        set
        {
            double.TryParse(value, out double parsed);
            calculator.SelectedUnit.Pressure = parsed;
            AppSettings.EndPressure = calculator.SelectedUnit.PressureBar;
        }
    }

    private string NitroxOxygenPressure
    {
        get
        {
            calculator.SelectedUnit.PressureBar = AppSettings.MaxOxygenPressure;
            return calculator.SelectedUnit.Pressure.ToString();
        }
        set
        {
            double.TryParse(value, out double parsed);
            calculator.SelectedUnit.Pressure = parsed;
            AppSettings.MaxOxygenPressure = calculator.SelectedUnit.PressureBar;
        }
    }

    private string NitroxStartMix
    {
        get => AppSettings.StartMix.ToString();
        set
        {
            double.TryParse(value, out double parsed);
            AppSettings.StartMix = parsed;
        }
    }

    private string NitroxEndMix
    {
        get => AppSettings.EndMix.ToString();
        set
        {
            double.TryParse(value, out double parsed);
            AppSettings.EndMix = parsed;
        }
    }

}