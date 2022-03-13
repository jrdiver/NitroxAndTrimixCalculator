using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;
using NitroxCalculatorMaui.Class;

namespace NitroxCalculatorMaui.Pages;

public partial class Equalize
{
    private PressureEqualization calculator;
    private PressureEqualizationData tank1;
    private PressureEqualizationData tank2;
    private PressureEqualizationData tankResult;

    public string Tank1Pressure
    {
        get => tank1.Pressure.ToString();
        set
        {
            double.TryParse(value, out double tankPressure);
            tank1.Pressure = tankPressure;
            EqualizeTanks();
        }
    }

    public string Tank2Pressure
    {
        get => tank2.Pressure.ToString();
        set
        {
            double.TryParse(value, out double tankPressure);
            tank2.Pressure = tankPressure;
            EqualizeTanks();
        }
    }

    public string Tank1Capacity
    {
        get => tank1.FullTankSize.ToString();
        set
        {
            double.TryParse(value, out double FullTankSize);
            tank1.FullTankSize = FullTankSize;
            EqualizeTanks();
        }
    }

    public string Tank2Capacity
    {
        get => tank2.FullTankSize.ToString();
        set
        {
            double.TryParse(value, out double FullTankSize);
            tank2.FullTankSize = FullTankSize;
            EqualizeTanks();
        }
    }

    protected override void OnInitialized() // = On Page Load
    {
        calculator = new();
        tank1 = new(500, 80, calculator.GetUnit(AppSettings.SelectedUnit));
        tank2 = new(2500, 80, calculator.GetUnit(AppSettings.SelectedUnit));
        EqualizeTanks();
    }

    private void EqualizeTanks()
    {
        tankResult = calculator.EqualizeTanks(tank1, tank2);
    }

}