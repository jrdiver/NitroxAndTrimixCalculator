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

        calculator.SelectedUnit.PressureBar = AppSettings.DefaultStartPressure;
        startPressure = calculator.SelectedUnit.Pressure;

        calculator.SelectedUnit.PressureBar = AppSettings.DefaultEndPressure;
        endPressure = calculator.SelectedUnit.Pressure;

        calculator.SelectedUnit.PressureBar = AppSettings.DefaultMaxOxygenPressure;
        maxO2Pressure = calculator.SelectedUnit.Pressure;

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
        string textResult = string.Empty;
        textResult += "<br><br>Max Depth at 1.3: " + calculator.MaxOperatingDepthCalculator(o2Percent, 1.3) + " " + result.SelectedUnit.DepthName + "<br>";
        textResult += "Max Depth at 1.4: " + calculator.MaxOperatingDepthCalculator(o2Percent, 1.4) + " " + result.SelectedUnit.DepthName + "<br>";
        textResult += "Max Depth at 1.5: " + calculator.MaxOperatingDepthCalculator(o2Percent, 1.5) + " " + result.SelectedUnit.DepthName + "<br>";
        textResult += "Max Depth at 1.6: " + calculator.MaxOperatingDepthCalculator(o2Percent, 1.6) + " " + result.SelectedUnit.DepthName + "<br>";
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