using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorTests
{
    [TestClass]
    public class LibraryTests
    {
        private readonly MixCalculator calculator = new();

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
            calculator.SelectedUnit.SetDepth(50);
            calculator.SelectedUnit.SetPressure(3500);
            Assert.AreEqual(15.24, Math.Round(calculator.SelectedUnit.DepthMeter, 3));
            Assert.AreEqual(241.317, Math.Round(calculator.SelectedUnit.PressureBar, 3));
        }

        [TestMethod]
        public void VerifyUnitImperialOutput()
        {
            calculator.LoadUnit("Imperial");
            calculator.SelectedUnit.SetDepth(50);
            calculator.SelectedUnit.SetPressure(3500);
            Assert.AreEqual(50, calculator.SelectedUnit.GetDepth());
            Assert.AreEqual(3500, calculator.SelectedUnit.GetPressure());
        }

        [TestMethod]
        public void LoadUnitMetric()
        {
            calculator.LoadUnit("Metric");
            Assert.AreEqual("Meter", calculator.SelectedUnit.DepthName);
        }

        [TestMethod]
        public void VerifyUnitMetricInput()
        {
            calculator.LoadUnit("Metric");
            calculator.SelectedUnit.SetDepth(50);
            calculator.SelectedUnit.SetPressure(60);
            Assert.AreEqual(50, Math.Round(calculator.SelectedUnit.DepthMeter, 3));
            Assert.AreEqual(60, Math.Round(calculator.SelectedUnit.PressureBar, 3));
        }

        [TestMethod]
        public void VerifyUnitMetricOutput()
        {
            calculator.LoadUnit("Metric");
            calculator.SelectedUnit.SetDepth(50);
            calculator.SelectedUnit.SetPressure(60);
            Assert.AreEqual(50, calculator.SelectedUnit.GetDepth());
            Assert.AreEqual(60, calculator.SelectedUnit.GetPressure());
        }

        [TestMethod]
        public void CalculateEmpty()
        {
            calculator.LoadUnit("Imperial");
            MixInputs input = new();
            input.SetEndMix(36);
            input.SetEndPressure(3200);
            input.SetTopOffMix(21);
            MixResult output = calculator.CalculateFromEmpty(input);

            Assert.AreEqual(607.595, Math.Round(output.AddOxygen, 3));
        }

        [TestMethod]
        public void TopUp()
        {
            calculator.LoadUnit("Imperial");
            MixInputs input = new();
            input.SetStartMix(36);
            input.SetStartPressure(1000);
            input.SetEndPressure(3400);
            input.SetTopOffMix(21);
            double output = calculator.TopUp(input);

            Assert.AreEqual(25.412, Math.Round(output, 3));
        }

        [TestMethod]
        public void MixFromMix()
        {
            calculator.LoadUnit("Imperial");
            MixInputs input = new();
            input.SetStartMix(32);
            input.SetEndMix(36);
            input.SetStartPressure(500);
            input.SetEndPressure(3400);
            input.SetTopOffMix(21);
            MixResult output = calculator.MixFromExistingMix(input);

            Assert.AreEqual(575.949, Math.Round(output.AddOxygen, 3));
        }

        [TestMethod]
        public void ReverseTopUp()
        {
            calculator.LoadUnit("Imperial");
            MixInputs input = new();
            input.SetStartMix(36);
            input.SetEndPressure(3400);
            input.SetTopOffMix(21);
            input.SetEndMix(25);
            double output = calculator.ReverseTopUp(input);

            Assert.AreEqual(906.667, Math.Round(output, 3));
        }

        [TestMethod]
        public void ModCalculator()
        {
            calculator.LoadUnit("Imperial");
            double output = calculator.MaxOperatingDepthCalculator(36, 1.6);

            Assert.AreEqual(113.667, Math.Round(output, 3));
        }

        [TestMethod]
        public void BestMix()
        {
            calculator.LoadUnit("Imperial");
            double output = calculator.BestMixForDepth(1.4, 70);

            Assert.AreEqual(44.854, Math.Round(output, 3));
        }

        [TestMethod]
        public void EquivalentAirDepth()
        {
            calculator.LoadUnit("Imperial");
            double output = calculator.EquivalentAirDepth(36, 100);

            Assert.AreEqual(74.747, Math.Round(output, 3));
        }
    }
}
