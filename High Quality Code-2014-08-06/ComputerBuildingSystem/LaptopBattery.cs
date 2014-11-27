namespace ComputersBuildingSystemCore
{
    using System;

    internal class LaptopBattery
    {
        private const int DefaultBatteryCharge = 50;

        internal LaptopBattery()
        {
            this.Percentage = LaptopBattery.DefaultBatteryCharge;
        }

        public int Percentage { get; private set; }

        public void Charge(int percentage)
        {
            this.Percentage += percentage;

            if (this.Percentage > 100)
            {
                this.Percentage = 100;
            }

            if (this.Percentage < 0)
            {
                this.Percentage = 0;
            }
        }
    }
}