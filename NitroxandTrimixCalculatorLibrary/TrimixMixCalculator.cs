using System.Collections.Generic;
using NitroxAndTrimixCalculatorLibrary.Class;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorLibrary;

public class TrimixMixCalculator
{
    public Unit SelectedUnit { get; }
    public List<Unit> UnitList { get; }

    public TrimixMixCalculator()
    {
        UnitList = LoadUnits.LoadAllUnits();
        if (UnitList.Count > 0)
        {
            SelectedUnit = UnitList[0];
        }
    }
}