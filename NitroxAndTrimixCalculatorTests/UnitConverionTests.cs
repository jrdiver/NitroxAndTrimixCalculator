using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorTests;

[TestClass]
public class UnitConverionTests
{
    private readonly NitroxMixCalculator calculator = new();

    [TestMethod]
    public void LoadUnitImperial()
    {
        calculator.LoadUnit("Imperial");
        Assert.AreEqual("Feet", calculator.SelectedUnit.DepthName);
    }

    [TestMethod]
    public void VerifyUnitImperialInput()
    {
        calculator.LoadUnit("Imperial");
        calculator.SelectedUnit.Depth = 50;
        calculator.SelectedUnit.Pressure = 3500;
        Assert.AreEqual(15.24, Math.Round(calculator.SelectedUnit.DepthMeter, 3));
        Assert.AreEqual(241.317, Math.Round(calculator.SelectedUnit.PressureBar, 3));
    }

    [TestMethod]
    public void VerifyUnitImperialOutput()
    {
        calculator.LoadUnit("Imperial");
        calculator.SelectedUnit.Depth = 50;
        calculator.SelectedUnit.Pressure = 3500;
        Assert.AreEqual(50, calculator.SelectedUnit.Depth);
        Assert.AreEqual(3500, calculator.SelectedUnit.Pressure);
    }

    [TestMethod]
    public void LoadUnitMetric()
    {
        calculator.LoadUnit("Metric");
        Assert.AreEqual("Meters", calculator.SelectedUnit.DepthName);
    }

    [TestMethod]
    public void VerifyUnitMetricInput()
    {
        calculator.LoadUnit("Metric");
        calculator.SelectedUnit.Depth = 50;
        calculator.SelectedUnit.Pressure = 60;
        Assert.AreEqual(50, Math.Round(calculator.SelectedUnit.DepthMeter, 3));
        Assert.AreEqual(60, Math.Round(calculator.SelectedUnit.PressureBar, 3));
    }

    [TestMethod]
    public void VerifyUnitMetricOutput()
    {
        calculator.LoadUnit("Metric");
        calculator.SelectedUnit.Depth = 50;
        calculator.SelectedUnit.Pressure = 60;
        Assert.AreEqual(50, calculator.SelectedUnit.Depth);
        Assert.AreEqual(60, calculator.SelectedUnit.Pressure);
    }

    [TestMethod]
    public void ModCalculator()
    {
        calculator.LoadUnit("Imperial");
        double output = calculator.MaxOperatingDepthCalculator(36, 1.6);

        Assert.AreEqual(113, Math.Round(output, 3));
    }

    [TestMethod]
    public void EquivalentAirDepth()
    {
        calculator.LoadUnit("Imperial");
        double output = calculator.EquivalentAirDepth(36, 100);

        Assert.AreEqual(74.8, Math.Round(output, 3));
    }

    [TestMethod]
    public void VerifyMetricUnitConversion()
    {
        calculator.LoadUnit("Metric");
        calculator.SelectedUnit.Depth = 20;
        calculator.SelectedUnit.Pressure = 20;
        calculator.SelectedUnit.Volume = 20;

        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.Depth, 3));
        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.Pressure, 3));
        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.Volume, 3));
        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.DepthMeter, 3));
        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.PressureBar, 3));
        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.VolumeLiter, 3));

        calculator.SelectedUnit.Depth = 500;
        calculator.SelectedUnit.Pressure = 500;
        calculator.SelectedUnit.Volume = 500;

        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.Depth, 3));
        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.Pressure, 3));
        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.Volume, 3));
        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.DepthMeter, 3));
        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.PressureBar, 3));
        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.VolumeLiter, 3));
    }

    [TestMethod]
    public void VerifyImperialUnitConversion()
    {
        calculator.LoadUnit("Imperial");
        calculator.SelectedUnit.Depth = 20;
        calculator.SelectedUnit.Pressure = 20;
        calculator.SelectedUnit.Volume = 20;

        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.Depth, 3));
        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.Pressure, 3));
        Assert.AreEqual(20, Math.Round(calculator.SelectedUnit.Volume, 3));
        Assert.AreEqual(6.096, Math.Round(calculator.SelectedUnit.DepthMeter, 3));
        Assert.AreEqual(1.379, Math.Round(calculator.SelectedUnit.PressureBar, 3));
        Assert.AreEqual(566.336, Math.Round(calculator.SelectedUnit.VolumeLiter, 3));

        calculator.SelectedUnit.Depth = 500;
        calculator.SelectedUnit.Pressure = 500;
        calculator.SelectedUnit.Volume = 500;

        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.Depth, 3));
        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.Pressure, 3));
        Assert.AreEqual(500, Math.Round(calculator.SelectedUnit.Volume, 3));
        Assert.AreEqual(152.402, Math.Round(calculator.SelectedUnit.DepthMeter, 3));
        Assert.AreEqual(34.474, Math.Round(calculator.SelectedUnit.PressureBar, 3));
        Assert.AreEqual(14158.41, Math.Round(calculator.SelectedUnit.VolumeLiter, 3));
    }
}
