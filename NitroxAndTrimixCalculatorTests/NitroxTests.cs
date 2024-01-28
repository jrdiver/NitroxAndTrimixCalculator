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
    public void BestMix()
    {
        calculator.LoadUnit("Imperial");
        double output = calculator.BestMixForDepth(1.4, 70);

        Assert.AreEqual(44.677, Math.Round(output, 3));
    }
}
