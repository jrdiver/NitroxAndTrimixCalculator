namespace NitroxAndTrimixCalculatorLibrary.Object
{
    public class MixResult
    {
        public double RemoveGas;
        public Unit SelectedUnit = new Unit();
        public double AddOxygen;
        public double AddHelium;
        public MixInputs Inputs = new MixInputs();

        public double AddTopOffGas()
        {
            double output = Inputs.EndPressure + RemoveGas - AddHelium - AddOxygen - Inputs.StartPressure;
            return output;
        }

        public bool ValidMix()
        {
            return !(RemoveGas >= 0 && AddHelium >= 0 && AddOxygen >= 0 && RemoveGas <= Inputs.StartPressure);
        }

    }
}
