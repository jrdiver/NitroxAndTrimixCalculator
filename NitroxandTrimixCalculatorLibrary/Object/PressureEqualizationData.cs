namespace NitroxAndTrimixCalculatorLibrary.Object;

public class PressureEqualizationData
{
    public double PressureBar;
    public double TankSizeLiter;
    public double TankSizePressure;
    public Unit SelectedUnit;

    public double Pressure
    {
        get
        {
            SelectedUnit.PressureBar = PressureBar;
            return SelectedUnit.Pressure;
        }
        set
        {
            SelectedUnit.Pressure = value;
            PressureBar = SelectedUnit.PressureBar;
        }
    }

    public double SizePressure
    {
        get
        {
            SelectedUnit.PressureBar = TankSizePressure;
            return SelectedUnit.Pressure;
        }
        set
        {
            SelectedUnit.Pressure = value;
            TankSizePressure = SelectedUnit.PressureBar;
        }
    }

    public double TankSize
    {
        get
        {
            SelectedUnit.VolumeLiter = TankSizeLiter;
            return SelectedUnit.Volume;
        }
        set
        {
            SelectedUnit.Volume = value;
            TankSizeLiter = SelectedUnit.VolumeLiter;
        }
    }


    public PressureEqualizationData(double pressure, double tankSize, Unit selectedUnit)
    {
        SelectedUnit = selectedUnit;
        Pressure = pressure;
        TankSize = tankSize;
    }
}