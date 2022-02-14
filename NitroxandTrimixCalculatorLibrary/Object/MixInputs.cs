using System;
using System.Diagnostics;
using NitroxAndTrimixCalculatorLibrary.Class;

namespace NitroxAndTrimixCalculatorLibrary.Object
{
    public class MixInputs
    {
        public double StartPressure { get; private set; } = 500;
        public double EndPressure { get; private set; } = 3400;
        public double MaxOxygenPressure { get; private set; } = 4500;
        public double StartMix { get; private set; } = 32;
        public double EndMix { get; private set; } = 32;
        public double TopOffMix { get; private set; } = 20.9;
        public double MaxPressure { get; private set; } = 10000000000000000000;

        private bool error = false;
        public bool EnableMaxOxygenPressure = false;

        public bool SetStartPressure(double input)
        {
            StartPressure = DataVerify.VerifyDouble(input, 0, MaxPressure);
            return Math.Abs(input - StartPressure) < .01;
        }

        public bool SetEndPressure(double input)
        {
            EndPressure = DataVerify.VerifyDouble(input, 0, MaxPressure);
            return Math.Abs(input - EndPressure) < .01;
        }

        public bool SetMaxOxygenPressure(double input)
        {
            MaxOxygenPressure = DataVerify.VerifyDouble(input, 0, MaxPressure);
            return Math.Abs(input - MaxOxygenPressure) < .01;
        }

        public void SetMaxPressure(double input)
        {
            MaxPressure = Math.Abs(input);
        }

        /// <summary> Sets Start Mix Percentage </summary>
        public bool SetStartMix(double input)
        {
            StartMix = VerifyMix(input);
            return error;
        }

        public bool SetEndMix(double input)
        {
            EndMix = VerifyMix(input);
            return error;
        }

        public bool SetTopOffMix(double input)
        {
            TopOffMix = VerifyMix(input);
            return error;
        }

        public double PressureDifference()
        {
            return Math.Abs(EndPressure - StartPressure);
        }

        public bool EndPressureLarger()
        {
            return StartPressure <= EndPressure;
        }

        public double StartMixDecimal()
        {
            return StartMix / 100;
        }

        public double EndMixDecimal()
        {
            return EndMix / 100;
        }

        public double TopOffMixDecimal()
        {
            return TopOffMix / 100;
        }

        public string GetTopOffGasName()
        {
            return PercentToName(TopOffMix);
        }

        public string GeStartGasName()
        {
            return PercentToName(StartMix);
        }

        public string GetEndGasName()
        {
            return PercentToName(EndMix);
        }

        private string PercentToName(double percent)
        {
            return percent switch
            {
                > 20.8 and < 21.1 => "Air",
                > 99.8 => "Oxygen",
                _ => percent + "%"
            };
        }

        #region PrivateMethoods
        private double VerifyMix(double input)
        {
            error = false;
            double output = DataVerify.VerifyDouble(input, 0, 100);
            if (output < 1 && output > 0)
            {
                output *= 100;
                input *= 100;
            }

            if (Math.Abs(input - output) > .01)
            {
                error = true;
                Debug.WriteLine("Invalid Mix Input");
            }
            return output;
        }
        #endregion
    }
}
