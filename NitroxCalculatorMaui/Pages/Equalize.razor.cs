using Microsoft.AspNetCore.Components;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxCalculatorMaui.Pages;

public partial class Equalize
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
        }
    }
    
    public string StartMixProperty
    {
        get => startMix;
        set
        {
            startMix = value;
        }
    }

    public string EndMixProperty
    {
        get => endMix;
        set
        {
            endMix = value;
        }
    }

    public string EndPressureProperty
    {
        get => endPressure;
        set
        {
            endPressure = value;
        }
    }

    public string MaxO2PressureProperty
    {
        get => maxO2Pressure;
        set
        {
            maxO2Pressure = value;
        }
    }

    protected override void OnInitialized() // = On Page Load
    {
        calculator = new();
        calculator.LoadUnit("imperial");
    }
    
}