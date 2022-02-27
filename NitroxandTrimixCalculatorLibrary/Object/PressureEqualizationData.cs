namespace NitroxAndTrimixCalculatorLibrary.Object;

public class PressureEqualizationData
{
    public double Pressure;
    public double TankSize;
    public Unit SelectedUnit;

    public PressureEqualizationData(double pressure, double tankSize, Unit selectedUnit)
    {
        Pressure = pressure;
        TankSize = tankSize;
        SelectedUnit = selectedUnit;
    }

    public double GetPressureMetric()
    {
        SelectedUnit.Pressure=Pressure;
        return SelectedUnit.PressureBar;
    }

    public double GetTankSizeMetric()
    {
        SelectedUnit.Volume = TankSize;
        return SelectedUnit.VolumeLiter;
    }

    public void SetPressureMetric(double bar)
    {
        SelectedUnit.PressureBar = bar;
        Pressure = SelectedUnit.Pressure;
    }

    public void SetTankSizeMetric(double liters)
    {
        SelectedUnit.VolumeLiter = liters;
        TankSize = SelectedUnit.Volume;
    }
}