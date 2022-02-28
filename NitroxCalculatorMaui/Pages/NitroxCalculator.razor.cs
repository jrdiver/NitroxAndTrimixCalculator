using Microsoft.AspNetCore.Components;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;
using NitroxCalculatorMaui.Class;

namespace NitroxCalculatorMaui.Pages;

public partial class NitroxCalculator
{
    private NitroxMixCalculator calculator;
    private double startPressure = 500;
    private double startMix = 32;
    private double endPressure = 3400;
    private double endMix = 32;
    private double maxO2Pressure = 4500;
    private MarkupString output;
    private MixResult result = new();

    public string StartPressure
    {
        get => startPressure.ToString();
        set
        {
            double.TryParse(value, out double pressure);
            startPressure = pressure;
            CalculateMix();
        }
    }

    public string StartMix
    {
        get => startMix.ToString();
        set
        {
            double.TryParse(value, out double mix);
            startMix = mix;
            CalculateMix();
        }
    }

    public string EndMix
    {
        get => endMix.ToString();
        set
        {
            double.TryParse(value, out double mix);
            endMix = mix;
            CalculateMix();
        }
    }

    public string EndPressure
    {
        get => endPressure.ToString();
        set
        {
            double.TryParse(value, out double pressure);
            endPressure = pressure;
            CalculateMix();
        }
    }

    public string MaxO2Pressure
    {
        get => maxO2Pressure.ToString();
        set
        {
            double.TryParse(value, out double pressure);
            maxO2Pressure = pressure;
            CalculateMix();
        }
    }

    protected override void OnInitialized() // = On Page Load
    {
        calculator = new();
        calculator.LoadUnit(AppSettings.SelectedUnit);

        calculator.SelectedUnit.PressureBar = AppSettings.StartPressure;
        startPressure = calculator.SelectedUnit.Pressure;

        calculator.SelectedUnit.PressureBar = AppSettings.EndPressure;
        endPressure = calculator.SelectedUnit.Pressure;

        calculator.SelectedUnit.PressureBar = AppSettings.MaxOxygenPressure;
        maxO2Pressure = calculator.SelectedUnit.Pressure;

        startMix = AppSettings.StartMix;
        endMix = AppSettings.EndMix;

        CalculateMix();
    }

    private void AirTopOff()
    {
        MixInputs input = LoadMixInputs();

        if (!input.EndPressureLarger)
        {
            output = new MarkupString("Start Pressure higher then End Pressure");
            return;
        }

        endMix = Math.Round(calculator.TopUp(input), 1);
        CalculateMix();
    }

    private void CalculateMix()
    {
        string textResult = string.Empty;
        MixInputs input = LoadMixInputs();

        result = calculator.CalculateMix(input);

        double pressure = Math.Round(result.Inputs.StartPressure);
        if (result.RemoveGas > 0)
        {
            pressure -= Math.Round(result.RemoveGas);
            textResult += "Drain " + Math.Round(result.RemoveGas) + " " + calculator.SelectedUnit.PressureName + " from the tank to " + pressure + " " + calculator.SelectedUnit.PressureName + "<br>";
        }
        if (result.AddOxygen > 0)
        {
            pressure += Math.Round(result.AddOxygen);
            textResult += "Add " + Math.Round(result.AddOxygen) + " " + calculator.SelectedUnit.PressureName + " of Oxygen to " + pressure + " " + calculator.SelectedUnit.PressureName + "<br>";
        }
        if (result.AddTopOffGas() > 0)
        {
            textResult += "Add " + Math.Round(result.AddTopOffGas()) + " " + calculator.SelectedUnit.PressureName + " of " + result.Inputs.GetTopOffGasName + " to " + result.Inputs.EndPressure + " " + calculator.SelectedUnit.PressureName + "<br>";
        }

        if (result.ValidMix())
        {
            textResult = "Not Possible with selected Inputs" + "<br>";
        }
        else
        {
            textResult += GetMod(result.Inputs.EndMix);
        }
        output = new(textResult);
    }

    private string GetMod(double o2Percent)
    {
        string textResult = AppSettings.P02List.Aggregate("<br><br>", (current, p02) => current + ($"Max Depth at {p02}: " + calculator.MaxOperatingDepthCalculator(o2Percent, p02) + " " + result.SelectedUnit.DepthName + "<br>"));

        AppSettings.P02List = new() { 1.3, 1.4, 1.5, 1.6 };
        List<double> number = AppSettings.P02List;
        return textResult;
    }

    private MixInputs LoadMixInputs()
    {
        if (startMix > 100)
        {
            startMix = 100;
        }

        MixInputs input = new()
        {
            EnableMaxOxygenPressure = true,
            StartPressure = startPressure,
            MaxOxygenPressure = maxO2Pressure,
            StartMix = startMix,
            EndPressure = endPressure,
            EndMix = endMix
        };

        startPressure = Math.Round(input.StartPressure);
        startMix = Math.Round(input.StartMix, 1);
        endPressure = Math.Round(input.EndPressure);
        endMix = Math.Round(input.EndMix, 1);
        maxO2Pressure = Math.Round(input.MaxOxygenPressure);

        return input;
    }
}