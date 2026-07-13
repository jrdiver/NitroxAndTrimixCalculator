using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxAndTrimixCalculatorLibrary;

namespace NitroxAndTrimixCalculatorTests;

[TestClass]
public class TrimixTests
{
    [TestMethod]
    public void TrimixMixCalculator_Constructor_LoadsUnits()
    {
        TrimixMixCalculator calculator = new();
        Assert.IsNotNull(calculator.SelectedUnit);
        Assert.IsTrue(calculator.UnitList.Count > 0);
    }
}
