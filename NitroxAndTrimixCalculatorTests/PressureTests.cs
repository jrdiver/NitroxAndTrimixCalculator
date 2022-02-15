using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorTests;

[TestClass]
public class PressureTests
{
    private readonly PressureEqualization calculator = new();

    [TestMethod]
    public void LoadUnitImperial()
    {
        PressureEqualizationData tank = new(3000, 80, calculator.GetUnit("imperial"));

        Assert.AreEqual(2265.346, Math.Round(tank.GetTankSizeMetric(), 3));
        Assert.AreEqual(206.843, Math.Round(tank.GetPressureMetric(), 3));
    }

    [TestMethod]
    public void EqualizeTanks()
    {
        PressureEqualizationData tank1 = new(3000, 40, calculator.GetUnit("imperial"));
        PressureEqualizationData tank2 = new(0, 80, calculator.GetUnit("imperial"));

        PressureEqualizationData output = calculator.EqualizeTanks(tank1, tank2);

        Assert.AreEqual(3398.018, Math.Round(output.GetTankSizeMetric(), 3));
        Assert.AreEqual(68.948, Math.Round(output.GetPressureMetric(), 3));
        Assert.AreEqual(120, Math.Round(output.TankSize, 3));
        Assert.AreEqual(1000, Math.Round(output.Pressure, 3));
    }
}