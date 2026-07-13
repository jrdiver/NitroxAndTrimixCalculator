using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxAndTrimixCalculatorLibrary.Class;

namespace NitroxAndTrimixCalculatorTests;

[TestClass]
public class DataVerifyTests
{
    [TestMethod]
    public void VerifyDouble_LessThanLow_ReturnsLow()
    {
        double result = DataVerify.Verify(5.0, 10.0, 20.0);
        Assert.AreEqual(10.0, result);
    }

    [TestMethod]
    public void VerifyDouble_GreaterThanHigh_ReturnsHigh()
    {
        double result = DataVerify.Verify(25.0, 10.0, 20.0);
        Assert.AreEqual(20.0, result);
    }

    [TestMethod]
    public void VerifyDouble_WithinRange_ReturnsValue()
    {
        double result = DataVerify.Verify(15.0, 10.0, 20.0);
        Assert.AreEqual(15.0, result);
    }

    [TestMethod]
    public void VerifyInt_LessThanLow_ReturnsLow()
    {
        int result = DataVerify.Verify(5, 10, 20);
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void VerifyInt_GreaterThanHigh_ReturnsHigh()
    {
        int result = DataVerify.Verify(25, 10, 20);
        Assert.AreEqual(20, result);
    }

    [TestMethod]
    public void VerifyInt_WithinRange_ReturnsValue()
    {
        int result = DataVerify.Verify(15, 10, 20);
        Assert.AreEqual(15, result);
    }

    [TestMethod]
    public void PercentToName_AirRange_ReturnsAir()
    {
        Assert.AreEqual("Air", DataVerify.PercentToName(20.9));
    }

    [TestMethod]
    public void PercentToName_OxygenRange_ReturnsOxygen()
    {
        Assert.AreEqual("Oxygen", DataVerify.PercentToName(100));
        Assert.AreEqual("Oxygen", DataVerify.PercentToName(99.9));
    }

    [TestMethod]
    public void PercentToName_CustomPercent_ReturnsPercentString()
    {
        Assert.AreEqual("32%", DataVerify.PercentToName(32));
    }

    [TestMethod]
    public void VerifyMix_ValidPercentage_ReturnsInput()
    {
        Assert.AreEqual(32.0, DataVerify.VerifyMix(32.0));
    }

    [TestMethod]
    public void VerifyMix_DecimalInput_ReturnsPercentage()
    {
        Assert.AreEqual(32.0, DataVerify.VerifyMix(0.32));
    }

    [TestMethod]
    public void VerifyMix_OutOfBounds_ReturnsBounded()
    {
        Assert.AreEqual(100.0, DataVerify.VerifyMix(150.0));
        Assert.AreEqual(0.0, DataVerify.VerifyMix(-10.0));
    }
}
