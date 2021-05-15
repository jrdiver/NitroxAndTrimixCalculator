namespace NitroxAndTrimixCalculatorLibrary.Object
{
    public class MixResult
    {
        public double RemoveGas;
        public Unit SelectedUnit = new Unit();
        public double AddOxygen;
        public double AddHelium;
        public double AddTopOffGas;
        public MixInputs Inputs = new MixInputs();
    }
}
