namespace ComputersBuildingSystemCore
{
    using ComputersBuildingSystemCore.Interfaces;

    internal class Cpu32 : Cpu
    {
        private const int LowerLimit = 0;
        private const int UpperLimit = 500;

        public Cpu32(byte numberOfCores, IMotherboard motherboard)
            : base(numberOfCores, motherboard)
        {
        }

        protected override bool ValidateNumber(int number)
        {
            var numberIsvalid = LowerLimit <= number && number <= UpperLimit;
            return numberIsvalid;
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
