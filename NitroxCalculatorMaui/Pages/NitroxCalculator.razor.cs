using Microsoft.AspNetCore.Components;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxCalculatorMaui.Pages;

public partial class NitroxCalculator
{
    private NitroxMixCalculator calculator;
    private string startPressure = "500"; 
    private string startMix = "32"; 
    private string endPressure = "3500"; 
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
    }

    private void CalculateMix()
    {
        string textResult = string.Empty;
        int.TryParse(startPressure, out int intStartPressure);
        int.TryParse(startMix, out int intStartMix);
        int.TryParse(endPressure, out int intendPressure);
        int.TryParse(endMix, out int intendMix);
        int.TryParse(maxO2Pressure, out int intMaxO2Pressure);

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
            textResult += "Add " + Math.Round(result.AddTopOffGas()) + " " + calculator.SelectedUnit.PressureName + " of " + result.Inputs.TopOffMix + "% to " + result.Inputs.EndPressure + " " + calculator.SelectedUnit.PressureName + "<br>";
        }

        if (result.ValidMix())
        {
            textResult = "Not Possible with selected Inputs" + "<br>";
        }
        else
        {
            textResult += "<br><br>Max Depth at 1.3: " + calculator.MaxOperatingDepthCalculator(result.Inputs.EndMix, 1.3) + " " + result.SelectedUnit.DepthName + "<br>";
            textResult += "Max Depth at 1.4: " + calculator.MaxOperatingDepthCalculator(result.Inputs.EndMix, 1.4) + " " + result.SelectedUnit.DepthName + "<br>";
            textResult += "Max Depth at 1.5: " + calculator.MaxOperatingDepthCalculator(result.Inputs.EndMix, 1.5) + " " + result.SelectedUnit.DepthName + "<br>";
            textResult += "Max Depth at 1.6: " + calculator.MaxOperatingDepthCalculator(result.Inputs.EndMix, 1.6) + " " + result.SelectedUnit.DepthName + "<br>";
        }
        output = new(textResult);
    }
}