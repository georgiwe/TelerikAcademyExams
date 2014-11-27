namespace ComputersBuildingSystemCore
{
    using System;

    internal class AbstractComputerFactory
    {
        private ComputerManufacturer manufacturer;

        public AbstractComputerFactory(ComputerManufacturer manufacturer)
        {
            this.Manufacturer = manufacturer;
        }

        private ComputerManufacturer Manufacturer
        {
            get
            {
                return this.manufacturer;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Manufacturer cannot be null.");
                }

                this.manufacturer = value;
            }
        }

        public PersonalComputer BuildPersonalComputer()
        {
            var pc = this.Manufacturer.BuildPersonalComputer();
            return pc;
        }

        public Laptop BuildLaptopComputer()
        {
            var laptop = this.Manufacturer.BuildLaptopComputer();
            return laptop;
        }

        public Server BuildServerComputer()
        {
            var server = this.Manufacturer.BuildServerComputer();
            return server;
        }
    }
}
