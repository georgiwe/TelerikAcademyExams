namespace ComputersBuildingSystemCore
{
    using System;

    using ComputersBuildingSystemCore.Interfaces;

    internal class Server : Computer
    {
        public Server(Cpu cpu, StorageProvider storageProvider, IMotherboard motherboard)
            : base(cpu, storageProvider, motherboard)
        {
        }

        internal void ProcessRequest(int data)
        {
            this.Motherboard.SaveRamValue(data);
            var operationWasSuccessful = this.Cpu.SquareNumber();

            if (operationWasSuccessful)
            {
                var result = this.Motherboard.LoadRamValue();
                this.Motherboard.DrawOnVideoCard(string.Format("Square of {0} is {1}.", data, result));
            }
        }
    }
}
