using System;
using System.Collections.Generic;
using System.Linq;
using NitroxAndTrimixCalculatorLibrary.Class;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxAndTrimixCalculatorLibrary
{
    public class NitroxMixCalculator
    {
        public Unit SelectedUnit { get; private set; }
        public List<Unit> UnitList { get; private set; }

        public NitroxMixCalculator()
        {
            UnitList = LoadUnits.LoadAllUnits();
            if (UnitList.Count > 0)
            {
                SelectedUnit = UnitList[0];
            }
        }

        public void AddUnit(Unit unit)
        {
            UnitList.Add(unit);
            UnitList = UnitList.OrderBy(x => x.Name).ToList();
        }

        public MixResult CalculateMix(MixInputs input)
        {
            //Run Standard Calculation
            MixResult result = MixFromExistingMix(input);
            //If removing oxygen, mix needs to be drained a bit, and if NaN, probably all of it needs to be drained
            if (result.AddOxygen < 0 || double.IsNaN(result.AddOxygen))
            {
                result.AddOxygen = 0;
                result.RemoveGas = ReverseTopUp(input);
            }

            //If amount of Oxygen added plus the start pressure is over end pressure...still invalid.  Probably a better way to do this, but brute force by draining pressure 25 at a time
            else if (result.AddOxygen + result.Inputs.StartPressure > result.Inputs.EndPressure)
            {
                input.SetStartPressure(input.StartPressure - 25);
                result = CalculateMix(input);
                result.Inputs.SetStartPressure(input.StartPressure + 25);
                result.RemoveGas += 25;
            }

            //What if you only have only 1000psi of oxygen?  System to get you close to your max pressure.
            if (result.Inputs.EnableMaxOxygenPressure && result.AddOxygen + result.Inputs.StartPressure > result.Inputs.MaxOxygenPressure)
            {
                double overPressure = (result.AddOxygen + result.Inputs.StartPressure - result.Inputs.MaxOxygenPressure) * 1.1;

                if (overPressure > 0 && result.Inputs.StartPressure == 0)
                {
                    result.Invalid = true;
                    return result;
                }

                input.SetStartPressure(input.StartPressure - overPressure);
                result = CalculateMix(input);
                result.Inputs.SetStartPressure(input.StartPressure + overPressure);
                result.RemoveGas += overPressure;
            }

            result.SelectedUnit = SelectedUnit;
            return result;
        }

        /// <summary> Loads a Unit By Name </summary>
        public void LoadUnit(string unitName)
        {
            List<Unit> nextUnit = UnitList.Where(x => string.Equals(x.Name, unitName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            if (nextUnit.Count > 0)
            {
                SelectedUnit = nextUnit[0];
            }
        }

        /// <summary>Calculates a mix assuming an empty tank,  Start Mix is ignored. </summary>
        public MixResult CalculateFromEmpty(MixInputs input)
        {
            input.SetStartPressure(0);
            MixResult result = new MixResult
            {
                AddOxygen = (input.EndMix - input.TopOffMix) / (100 - input.TopOffMix) * input.EndPressure,
                SelectedUnit = SelectedUnit,
                AddHelium = 0,
                Inputs = input
            };
            return result;
        }

        /// <summary> Calculates what percent your mix will be if you just top up with selected Top Off Mix </summary>
        public double TopUp(MixInputs input)
        {
            return (input.StartMixDecimal() * input.StartPressure + input.TopOffMixDecimal() * (input.EndPressure - input.StartPressure)) / input.EndPressure * 100;
        }

        /// <summary> Calculates a Nitrox Mix starting with a previous mix.  Invalid values are reverted to their nearest valid value </summary>
        public MixResult MixFromExistingMix(MixInputs input)
        {
            MixResult result = new MixResult { SelectedUnit = SelectedUnit };

            double intermediateMix = (input.EndMixDecimal() * input.EndPressure - input.StartMixDecimal() * input.StartPressure) / (input.EndPressure - input.StartPressure);

            result.AddOxygen = (intermediateMix - input.TopOffMixDecimal()) / (1 - input.TopOffMixDecimal()) * (input.EndPressure - input.StartPressure);
            result.Inputs = input;
            return result;
        }

        /// <summary> Calculates what pressure at the selected percentage needs to be in a tank to be able to use the selected Top Off Mix to top off to selected percent </summary>
        public double ReverseTopUp(MixInputs input)
        {
            return input.StartPressure - (input.EndMixDecimal() * input.EndPressure - input.TopOffMixDecimal() * input.EndPressure) / (input.StartMixDecimal() - input.TopOffMixDecimal());
        }

        /// <summary> Calculates the Max Operating Depth (MOD) of the Selected Mix and Partial Pressure.  Runs of metric internally and converts to selected unit</summary>
        public double MaxOperatingDepthCalculator(double mix, double partialPressure)
        {
            if (mix > 1)
            {
                mix /= 100;
            }
            double maxDepth = (partialPressure / mix - 1) * 10;
            SelectedUnit.SetDepthInMeters(maxDepth);
            return SelectedUnit.GetDepth();
        }

        public double EquivalentAirDepth(double mix, double depth)
        {
            SelectedUnit.SetDepth(depth);

            if (mix > 1)
            {
                mix /= 100;
            }
            SelectedUnit.SetDepthInMeters((1 - mix) * (SelectedUnit.DepthMeter + 10) / .79 - 10);
            return SelectedUnit.GetDepth();
        }

        public double BestMixForDepth(double partialPressure, double depth)
        {
            SelectedUnit.SetDepth(depth);
            return partialPressure / ((SelectedUnit.DepthMeter + 10) / 10) * 100;
        }
    }
}