using Microsoft.AspNetCore.Components;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxCalculatorMaui.Pages;

public partial class NitroxCalculator
{
    private NitroxMixCalculator calculator;
    private string startPressure = "500";
    private string startMix = "32";
    private string endPressure = "3400";
    private string endMix = "32";
    private string maxO2Pressure = "4500";
    private MarkupString output;
    private MixResult result = new();

    public string StartPressureProperty
    {
        get => startPressure;
        set
        {
            startPressure = value;
            CalculateMix();
        }
    }

    public string StartMixProperty
    {
        get => startMix;
        set
        {
            startMix = value;
            CalculateMix();
        }
    }

    public string EndMixProperty
    {
        get => endMix;
        set
        {
            endMix = value;
            CalculateMix();
        }
    }

    public string EndPressureProperty
    {
        get => endPressure;
        set
        {
            endPressure = value;
            CalculateMix();
        }
    }

    public string MaxO2PressureProperty
    {
        get => maxO2Pressure;
        set
        {
            maxO2Pressure = value;
            CalculateMix();
        }
    }

    protected override void OnInitialized() // = On Page Load
    {
        calculator = new();
        calculator.LoadUnit("imperial");

        CalculateMix();
    }

    private void AirTopOff()
    {
        double.TryParse(startPressure, out double intStartPressure);
        double.TryParse(startMix, out double intStartMix);
        double.TryParse(endPressure, out double intendPressure);
        double.TryParse(maxO2Pressure, out double intMaxO2Pressure);

        MixInputs input = new()
        {
            EnableMaxOxygenPressure = true
        };
        input.SetStartPressure(intStartPressure);
        input.SetMaxOxygenPressure(intMaxO2Pressure);
        input.SetStartMix(intStartMix);
        input.SetEndPressure(intendPressure);

        if (intStartPressure > intendPressure)
        {
            output = new MarkupString("Start Pressure higher then End Pressure");
            return;
        }

        double endMixPercent = Math.Round(calculator.TopUp(input), 1);
        endMix = endMixPercent.ToString();

        string textResult = "Add " + (intendPressure - intStartPressure) + " " + calculator.SelectedUnit.PressureName + " of " + result.Inputs.GetTopOffGasName() + " to " + result.Inputs.EndPressure + " " + calculator.SelectedUnit.PressureName + "<br>";
        textResult += GetMod(endMixPercent);
        output = new(textResult);
    }

    private void CalculateMix()
    {
        string textResult = string.Empty;
        double.TryParse(startPressure, out double intStartPressure);
        double.TryParse(startMix, out double intStartMix);
        double.TryParse(endPressure, out double intendPressure);
        double.TryParse(endMix, out double intendMix);
        double.TryParse(maxO2Pressure, out double intMaxO2Pressure);

        MixInputs input = new()
        {
            EnableMaxOxygenPressure = true
        };
        input.SetStartPressure(intStartPressure);
        input.SetMaxOxygenPressure(intMaxO2Pressure);
        input.SetStartMix(intStartMix);
        input.SetEndPressure(intendPressure);
        input.SetEndMix(intendMix);

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
            textResult += "Add " + Math.Round(result.AddTopOffGas()) + " " + calculator.SelectedUnit.PressureName + " of " + result.Inputs.GetTopOffGasName() + " to " + result.Inputs.EndPressure + " " + calculator.SelectedUnit.PressureName + "<br>";
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
}