namespace ComputersBuildingSystemCore
{
    using ComputersBuildingSystemCore.Interfaces;

    internal class Cpu64 : Cpu
    {
        private const int LowerLimit = 0;
        private const int UpperLimit = 1000;

        public Cpu64(byte numberOfCores, IMotherboard motherboard)
            : base(numberOfCores, motherboard)
        {
        }

        protected override bool ValidateNumber(int number)
        {
            var numberIsValid = true;

            if (number < LowerLimit ||
                number > UpperLimit)
            {
                numberIsValid = false;
            }

            return numberIsValid;
        }

        protected override string DecideErrorMessage(int number)
        {
            var errorMessage = string.Empty;

            if (number < LowerLimit)
            {
                errorMessage = "Number too low.";
            }
            else if (number > UpperLimit)
            {
                errorMessage = "Number too high.";
            }

            return errorMessage;
        }
    }
}
