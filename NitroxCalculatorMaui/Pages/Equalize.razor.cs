using Microsoft.AspNetCore.Components;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxCalculatorMaui.Pages;

public partial class Equalize
{
    private PressureEqualization calculator;
    internal PressureEqualizationData tank1;
    private PressureEqualizationData tank2;
    private PressureEqualizationData tankResult;

    public string Tank1Pressure
    {
        get => tank1.Pressure.ToString();
        set => double.TryParse(value, out tank1.Pressure);
    }

    public string Tank2Pressure
    {
        get => tank2.Pressure.ToString();
        set => double.TryParse(value, out tank2.Pressure);
    }

    public string Tank1Capacity
    {
        get => tank1.TankSize.ToString();
        set => double.TryParse(value, out tank1.TankSize);
    }

    public string Tank2Capacity
    {
        get => tank2.TankSize.ToString();
        set => double.TryParse(value, out tank2.TankSize);
    }

    protected override void OnInitialized() // = On Page Load
    {
        calculator = new();
        tank1 = new(500, 80, calculator.GetUnit("Imperial"));
        tank2 = new(2500, 80, calculator.GetUnit("Imperial"));
        EqualizeTanks();
    }

    private void EqualizeTanks()
    {
        tankResult = calculator.EqualizeTanks(tank1, tank2);
    }

}