using System;

namespace NitroxAndTrimixCalculatorLibrary.Object
{
    public class Unit
    {
        public string Name = string.Empty;
        public string PressureName = string.Empty;
        public string DepthName = string.Empty;
        public double UnitPerBar = 1;
        public double UnitPerMeter = 1;
        public int RoundPressureTo = 1;
        public int RoundDepthTo = 1;


        public double DepthMeter { get; private set; }
        public double PressureBar { get; private set; }

        public double GetDepth()
        {
            return Math.Round(DepthMeter * UnitPerMeter, RoundDepthTo);
        }

        public void SetDepth(double depth)
        {
            DepthMeter = depth / UnitPerMeter;
        }

        public void SetDepthInMeters(double depth)
        {
            DepthMeter = depth;
        }

        public double GetPressure()
        {
            return Math.Round(PressureBar * UnitPerBar, RoundPressureTo);
        }

        public void SetPressure(double pressure)
        {
            PressureBar = pressure / UnitPerBar;
        }

        public void SetPressureInBars(double pressure)
        {
            PressureBar = pressure;
        }
    }
}
