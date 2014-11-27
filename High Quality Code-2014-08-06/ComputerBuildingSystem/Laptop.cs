namespace ComputersBuildingSystemCore
{
    using System;

    using ComputersBuildingSystemCore.Interfaces;

    internal class Laptop : Computer
    {
        private LaptopBattery battery;

        public Laptop(Cpu cpu, StorageProvider storageProvider, IMotherboard motherboard, LaptopBattery battery)
            : base(cpu, storageProvider, motherboard)
        {
            this.Battery = battery;
        }

        protected LaptopBattery Battery
        {
            get
            {
                return this.battery;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Battery cannto be null.");
                }

                this.battery = value;
            }
        }

        public void ChargeBattery(int percentage)
        {
            this.Battery.Charge(percentage);
            this.Motherboard.DrawOnVideoCard(string.Format("Battery status: {0}%", this.battery.Percentage));
        }
    }
}
