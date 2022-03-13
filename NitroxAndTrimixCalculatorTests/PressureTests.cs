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

        Assert.AreEqual(2265.346, Math.Round(tank.FullTankSizeLiter, 3));
        Assert.AreEqual(206.843, Math.Round(tank.PressureBar, 3));
    }

    [TestMethod]
    public void EqualizeTanks()
    {
        PressureEqualizationData tank1 = new(3000, 40, calculator.GetUnit("imperial"));
        PressureEqualizationData tank2 = new(0, 80, calculator.GetUnit("imperial"));

        PressureEqualizationData output = calculator.EqualizeTanks(tank1, tank2);

        Assert.AreEqual(3398.018, Math.Round(output.FullTankSizeLiter, 3));
        Assert.AreEqual(1000, Math.Round(output.Pressure, 3));
        Assert.AreEqual(68.948, Math.Round(output.PressureBar, 3));
        Assert.AreEqual(120, Math.Round(output.FullTankSize, 3));
    }


    [TestMethod]
    public void PressureEqualizationData()
    {
        PressureEqualizationData tank = new(1500, 80, calculator.GetUnit("imperial"))
        {
            FullTankSize = 80,
            FullPressure = 3000
        };

        Assert.AreEqual(2265.346, Math.Round(tank.FullTankSizeLiter, 3));
        Assert.AreEqual(1500, Math.Round(tank.Pressure, 3));
        Assert.AreEqual(103.421, Math.Round(tank.PressureBar, 3));
        Assert.AreEqual(80, Math.Round(tank.FullTankSize, 3));
        Assert.AreEqual(0.4, Math.Round(tank.EmptyTankSize, 3));
        Assert.AreEqual(10.899, Math.Round(tank.EmptyTankSizeLiter, 3));
        Assert.AreEqual(40, Math.Round(tank.SizeAtCurrentPressure, 3));
        Assert.AreEqual(1132.673, Math.Round(tank.SizeAtCurrentPressureLiter, 3));
    }


    [TestMethod]
    public void ReverseEqualizeTanks()
    {
        PressureEqualizationData tank1 = new(1000, 80, calculator.GetUnit("imperial"));
        PressureEqualizationData combinedTanks = new(1750, 200, calculator.GetUnit("imperial"));

        PressureEqualizationData output = calculator.ReverseEqualizeTankOneTankAndEnding(tank1, combinedTanks);

        Assert.AreEqual(120, Math.Round(output.FullTankSize, 3));
        Assert.AreEqual(2250, Math.Round(output.Pressure, 3));

        tank1 = new(3000, 40, calculator.GetUnit("imperial"));
        combinedTanks = new(1000, 120, calculator.GetUnit("imperial"));

        output = calculator.ReverseEqualizeTankOneTankAndEnding(tank1, combinedTanks);

        Assert.AreEqual(80, Math.Round(output.FullTankSize, 3));
        Assert.AreEqual(0, Math.Round(output.Pressure, 3));
    }
}