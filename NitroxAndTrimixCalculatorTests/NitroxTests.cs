using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorTests;

[TestClass]
public class NitroxTests
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
    public void CalculateEmpty()
    {
        calculator.LoadUnit("Imperial");
        MixInputs input = new()
        {
            EndMix = 36,
            EndPressure = 3200,
            TopOffMix = 21
        };
        MixResult output = calculator.CalculateFromEmpty(input);

        Assert.AreEqual(607.595, Math.Round(output.AddOxygen, 3));
    }

    [TestMethod]
    public void TopUp()
    {
        calculator.LoadUnit("Imperial");
        MixInputs input = new()
        {
            StartMix = 36,
            StartPressure = 1000,
            EndPressure = 3400,
            TopOffMix = 21
        };
        double output = calculator.TopUp(input);

        Assert.AreEqual(25.412, Math.Round(output, 3));
    }

    [TestMethod]
    public void TopUpOverpressure()
    {
        calculator.LoadUnit("Imperial");
        MixInputs input = new()
        {
            StartMix = 36,
            StartPressure = 5000,
            EndPressure = 3400,
            TopOffMix = 21
        };
        double output = calculator.TopUp(input);

        Assert.AreEqual(-1, Math.Round(output, 3));
    }

    [TestMethod]
    public void MixFromMix()
    {
        calculator.LoadUnit("Imperial");
        MixInputs input = new()
        {
            StartMix = 32,
            EndMix = 36,
            StartPressure = 500,
            EndPressure = 3400,
            TopOffMix = 21
        };
        MixResult output = calculator.MixFromExistingMix(input);

        Assert.AreEqual(575.949, Math.Round(output.AddOxygen, 3));
    }

    [TestMethod]
    public void ReverseTopUp()
    {
        calculator.LoadUnit("Imperial");
        MixInputs input = new()
        {
            StartMix = 50,
            EndPressure = 3400,
            StartPressure = 500,
            TopOffMix = 21,
            EndMix = 25
        };
        double output = calculator.ReverseTopUp(input);

        Assert.AreEqual(31.034, Math.Round(output, 3));
    }

    [TestMethod]
    public void LowOxygenCalculation()
    {
        calculator.LoadUnit("Imperial");
        MixInputs input = new()
        {
            StartMix = 32,
            EndPressure = 3400,
            StartPressure = 1500,
            TopOffMix = 21,
            EndMix = 32,
            EnableMaxOxygenPressure = true,
            MaxOxygenPressure = 1000
        };
        MixResult output = calculator.CalculateMix(input);

        Assert.AreEqual(388.235, Math.Round(output.AddOxygen, 3));
        Assert.AreEqual(888.235, Math.Round(output.RemoveGas, 3));
        Assert.AreEqual(2400, Math.Round(output.AddTopOffGas(), 3));
    }

    [TestMethod]
    public void LowOxygenNoneNeededCalculation()
    {
        calculator.LoadUnit("Imperial");
        MixInputs input = new()
        {
            StartMix = 50,
            EndPressure = 3400,
            StartPressure = 1500,
            TopOffMix = 21,
            EndMix = 32,
            EnableMaxOxygenPressure = true,
            MaxOxygenPressure = 500
        };
        MixResult output = calculator.CalculateMix(input);

        Assert.AreEqual(0, Math.Round(output.AddOxygen, 3));
        Assert.AreEqual(210.345, Math.Round(output.RemoveGas, 3));
        Assert.AreEqual(2110.345, Math.Round(output.AddTopOffGas(), 3));
    }

    [TestMethod]
    public void DrainTopUpToGetDesiredCalculation()
    {
        calculator.LoadUnit("Imperial");
        MixInputs input = new()
        {
            StartMix = 50,
            EndPressure = 3400,
            StartPressure = 1500,
            TopOffMix = 21,
            EndMix = 32,
            EnableMaxOxygenPressure = false,
            MaxOxygenPressure = 4500
        };
        MixResult output = calculator.CalculateMix(input);

        Assert.AreEqual(0, Math.Round(output.AddOxygen, 3));
        Assert.AreEqual(210.345, Math.Round(output.RemoveGas, 3));
        Assert.AreEqual(2110.345, Math.Round(output.AddTopOffGas(), 3));
    }

    [TestMethod]
    public void ModCalculator()
    {
        calculator.LoadUnit("Imperial");
        double output = calculator.MaxOperatingDepthCalculator(36, 1.6);

        Assert.AreEqual(113, Math.Round(output, 3));
    }

    [TestMethod]
    public void BestMix()
    {
        calculator.LoadUnit("Imperial");
        double output = calculator.BestMixForDepth(1.4, 70);

        Assert.AreEqual(44.677, Math.Round(output, 3));
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