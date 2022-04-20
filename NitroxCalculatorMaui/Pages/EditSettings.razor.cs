using System.Globalization;
using NitroxAndTrimixCalculatorLibrary;
using NitroxCalculatorMaui.Class;


namespace NitroxCalculatorMaui.Pages;

public partial class EditSettings
{
    private readonly NitroxMixCalculator calculator = new();

    private string SelectedUnit
    {
        get
        {
            calculator.LoadUnit(AppSettings.SelectedUnit);
            return AppSettings.SelectedUnit;

        }
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
            return calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);
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
            return calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);
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
            return calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);
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
        get => AppSettings.StartMix.ToString(CultureInfo.InvariantCulture);
        set
        {
            double.TryParse(value, out double parsed);
            AppSettings.StartMix = parsed;
        }
    }

    private string NitroxEndMix
    {
        get => AppSettings.EndMix.ToString(CultureInfo.InvariantCulture);
        set
        {
            double.TryParse(value, out double parsed);
            AppSettings.EndMix = parsed;
        }
    }

    private string EqualizationFullPressure
    {
        get
        {
            calculator.SelectedUnit.PressureBar = AppSettings.EqualizationTankFullPressure;
            return calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);
        }
        set
        {
            double.TryParse(value, out double parsed);
            calculator.SelectedUnit.Pressure = parsed;
            AppSettings.EqualizationTankFullPressure = calculator.SelectedUnit.PressureBar;
        }
    }

    private string EqualizationTank1Pressure
    {
        get
        {
            calculator.SelectedUnit.PressureBar = AppSettings.EqualizationTank1Pressure;
            return calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);
        }
        set
        {
            double.TryParse(value, out double parsed);
            calculator.SelectedUnit.Pressure = parsed;
            AppSettings.EqualizationTank1Pressure = calculator.SelectedUnit.PressureBar;
        }
    }

    private string EqualizationTank2Pressure
    {
        get
        {
            calculator.SelectedUnit.PressureBar = AppSettings.EqualizationTank2Pressure;
            return calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);
        }
        set
        {
            double.TryParse(value, out double parsed);
            calculator.SelectedUnit.Pressure = parsed;
            AppSettings.EqualizationTank2Pressure = calculator.SelectedUnit.PressureBar;
        }
    }

    private string EqualizationTankSize
    {
        get
        {
            calculator.SelectedUnit.VolumeLiter = AppSettings.EqualizationTankSize;
            return calculator.SelectedUnit.Volume.ToString(CultureInfo.InvariantCulture);
        }
        set
        {
            double.TryParse(value, out double parsed);
            calculator.SelectedUnit.Volume = parsed;
            AppSettings.EqualizationTankSize = calculator.SelectedUnit.VolumeLiter;
        }
    }
}