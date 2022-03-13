using System;
using System.Collections.Generic;
using System.Linq;
using NitroxAndTrimixCalculatorLibrary.Class;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorLibrary;

public class PressureEqualization
{
    public List<Unit> UnitList { get; private set; }

    /// <summary> Requires Metric or Imperial to be specified. </summary>
    public PressureEqualization()
    {
        UnitList = LoadUnits.LoadAllUnits();
    }

    /// <summary> Loads a Unit By Name </summary>
    public Unit GetUnit(string unitName)
    {
        List<Unit> nextUnit = UnitList.Where(x => string.Equals(x.Name, unitName, StringComparison.CurrentCultureIgnoreCase)).ToList();
        return nextUnit.Count > 0 ? nextUnit[0] : null;
    }

    public void AddUnit(Unit unit)
    {
        UnitList.Add(unit);
        UnitList = UnitList.OrderBy(x => x.Name).ToList();
    }

    /// <summary> Equalizes the pressure of the 2 tanks entered and returns using the first tanks units. </summary>
    public PressureEqualizationData EqualizeTanks(PressureEqualizationData tank1, PressureEqualizationData tank2)
    {
        PressureEqualizationData output = new(0, 0, 0, tank1.SelectedUnit)
        {
            FullTankSizeLiter = tank1.FullTankSizeLiter + tank2.FullTankSizeLiter
        };

        double pressureFromFirst = tank1.PressureBar * (tank1.FullTankSizeLiter / output.FullTankSizeLiter);
        double pressureFromSecond = tank2.PressureBar * (tank2.FullTankSizeLiter / output.FullTankSizeLiter);
        output.PressureBar = pressureFromFirst + pressureFromSecond;

        return output;
    }

    /// <summary> Calculate values for a tank, from the other tank and the end results. </summary>
    public PressureEqualizationData ReverseEqualizeTankOneTankAndEnding(PressureEqualizationData tank1, PressureEqualizationData endPressure)
    {
        PressureEqualizationData output = new(0, 0, 0, tank1.SelectedUnit)
        {
            FullTankSizeLiter = endPressure.FullTankSizeLiter - tank1.FullTankSizeLiter
        };

        double pressureFromFirst = tank1.PressureBar * (tank1.FullTankSizeLiter / endPressure.FullTankSizeLiter);
        double pressureFromSecond = endPressure.PressureBar - pressureFromFirst;
        output.PressureBar = pressureFromSecond / (output.FullTankSizeLiter / endPressure.FullTankSizeLiter);

        return output;
    }
}