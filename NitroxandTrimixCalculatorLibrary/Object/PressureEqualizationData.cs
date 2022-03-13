namespace NitroxAndTrimixCalculatorLibrary.Object;

public class PressureEqualizationData
{
    public double PressureBar;
    public double FullPressureBar;
    public double FullTankSizeLiter;
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

    public double FullPressure
    {
        get
        {
            SelectedUnit.PressureBar = FullPressureBar;
            return SelectedUnit.Pressure;
        }
        set
        {
            SelectedUnit.Pressure = value;
            FullPressureBar = SelectedUnit.PressureBar;
        }
    }

    public double FullTankSize
    {
        get
        {
            SelectedUnit.VolumeLiter = FullTankSizeLiter;
            return SelectedUnit.Volume;
        }
        set
        {
            SelectedUnit.Volume = value;
            FullTankSizeLiter = SelectedUnit.VolumeLiter;
        }
    }

    public double SizeAtCurrentPressureLiter
    {
        get
        {
            int addValue = 0;
            if (PressureBar == 0)
                addValue++;
            return (PressureBar + addValue) * FullTankSizeLiter / (FullPressureBar + addValue);
        }
    }

    public double SizeAtCurrentPressure
    {
        get
        {
            SelectedUnit.VolumeLiter = SizeAtCurrentPressureLiter;
            return SelectedUnit.Volume;
        }
    }

    public double EmptyTankSizeLiter
    {
        get => FullTankSizeLiter / (FullPressureBar + 1);
        set => FullTankSizeLiter = value / (FullPressureBar + 1);
    }

    public double EmptyTankSize
    {
        get
        {
            SelectedUnit.VolumeLiter = EmptyTankSizeLiter;
            return SelectedUnit.Volume;
        }
        set
        {
            SelectedUnit.Volume = value;
            EmptyTankSizeLiter = SelectedUnit.VolumeLiter;
        }
    }

    public PressureEqualizationData(double pressure, double tankSize, Unit selectedUnit)
    {
        SelectedUnit = selectedUnit;
        Pressure = pressure;
        FullTankSize = tankSize;
    }
}