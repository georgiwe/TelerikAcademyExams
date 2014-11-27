namespace ComputersBuildingSystemCore
{
    using ComputersBuildingSystemCore.Interfaces;

    internal class Cpu128 : Cpu
    {
        private const int LowerLimit = 0;
        private const int UpperLimit = 2000;

        public Cpu128(byte numOfCores, IMotherboard motherboard)
            : base(numOfCores, motherboard)
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
