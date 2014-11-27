namespace ComputersBuildingSystemCore
{
    using System;

    using ComputersBuildingSystemCore.Interfaces;

    internal abstract class Cpu
    {
        private static readonly Random Random = new Random();
        private IMotherboard motherboard;
        private byte numberOfCores;

        internal Cpu(byte numberOfCores, IMotherboard motherboard)
        {
            this.NumberOfCores = numberOfCores;
            this.Motherboard = motherboard;
        }

        protected IMotherboard Motherboard
        {
            get
            {
                return this.motherboard;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Motherboard passed cannot be null.");
                }

                this.motherboard = value;
            }
        }

        protected byte NumberOfCores
        {
            get
            {
                return this.numberOfCores;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid number of cores");
                }

                this.numberOfCores = value;
            }
        }

        public bool SquareNumber()
        {
            var number = this.ReadNumber();
            var numberIsValid = this.ValidateNumber(number);
            var operationWasSuccesful = true;

            if (numberIsValid)
            {
                var squaredNumber = number * number;
                this.Motherboard.SaveRamValue(squaredNumber);
            }
            else
            {
                var errorMessage = this.DecideErrorMessage(number);
                this.OutputError(errorMessage);
                operationWasSuccesful = false;
            }

            return operationWasSuccesful;
        }

        internal void GenerateRandomNumber(int lowerLimit, int upperLimit)
        {
            if (upperLimit < lowerLimit)
            {
                throw new ArgumentException("Upper limit should be greater than or equal to lower limit.");
            }

            var randomNumber = Random.Next(lowerLimit, upperLimit + 1);

            this.Motherboard.SaveRamValue(randomNumber);
        }

        protected virtual int ReadNumber()
        {
            var number = this.Motherboard.LoadRamValue();
            return number;
        }

        protected abstract bool ValidateNumber(int number);

        protected abstract string DecideErrorMessage(int number);

        protected virtual void OutputError(string errorMessage)
        {
            this.Motherboard.DrawOnVideoCard(errorMessage);
        }
    }
}