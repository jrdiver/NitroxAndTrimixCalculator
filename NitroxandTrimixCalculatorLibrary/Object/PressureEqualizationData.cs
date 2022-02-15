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
        SelectedUnit.SetPressure(Pressure);
        return SelectedUnit.PressureBar;
    }

    public double GetTankSizeMetric()
    {
        SelectedUnit.SetVolume(TankSize);
        return SelectedUnit.VolumeLiter;
    }

    public void SetPressureMetric(double bar)
    {
        SelectedUnit.SetPressureInBars(bar);
        Pressure = SelectedUnit.GetPressure();
    }

    public void SetTankSizeMetric(double liters)
    {
        SelectedUnit.SetVolumeInLiters(liters);
        TankSize = SelectedUnit.GetVolume();
    }
}