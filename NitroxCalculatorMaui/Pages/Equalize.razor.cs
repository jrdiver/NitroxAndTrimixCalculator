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
            double.TryParse(value, out double fullTankSize);
            tank1.FullTankSize = fullTankSize;
            EqualizeTanks();
        }
    }

    public string Tank2Capacity
    {
        get => tank2.FullTankSize.ToString();
        set
        {
            double.TryParse(value, out double fullTankSize);
            tank2.FullTankSize = fullTankSize;
            EqualizeTanks();
        }
    }
    public string Tank1MaxPressure
    {
        get => tank1.FullPressure.ToString();
        set
        {
            double.TryParse(value, out double maxPressure);
            tank1.FullPressure = maxPressure;
            EqualizeTanks();
        }
    }
    public string Tank2MaxPressure
    {
        get => tank2.FullPressure.ToString();
        set
        {
            double.TryParse(value, out double maxPressure);
            tank2.FullPressure = maxPressure;
            EqualizeTanks();
        }
    }

    protected override void OnInitialized() // = On Page Load
    {
        calculator = new();
        tank1 = new(500, 80, 3000, calculator.GetUnit(AppSettings.SelectedUnit));
        tank2 = new(2500, 80, 3000, calculator.GetUnit(AppSettings.SelectedUnit));

        tank1.FullTankSizeLiter = AppSettings.EqualizationTankSize;
        tank2.FullTankSizeLiter = AppSettings.EqualizationTankSize;
        tank1.FullPressureBar = AppSettings.EqualizationTankFullPressure;
        tank2.FullPressureBar = AppSettings.EqualizationTankFullPressure;
        tank1.PressureBar = AppSettings.EqualizationTank1Pressure;
        tank2.PressureBar = AppSettings.EqualizationTank2Pressure;

        EqualizeTanks();
    }

    private void EqualizeTanks()
    {
        tankResult = calculator.EqualizeTanks(tank1, tank2);
    }

}