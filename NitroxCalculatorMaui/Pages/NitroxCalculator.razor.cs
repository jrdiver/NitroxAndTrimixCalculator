using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxCalculatorMaui.Pages
{
    public partial class NitroxCalculator
    {
        private readonly NitroxMixCalculator calculator = new();
        
        private NitroxCalculator()
        {
            calculator.LoadUnit("imperial");
        }

        private void CalculateMix()
        {
            MixInputs input = new();
            MixResult result = calculator.CalculateMix(input);
        }
    }
}