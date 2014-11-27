namespace ComputersBuildingSystemCore
{
    using System;
    using System.Linq;

    using ComputersBuildingSystemCore.Interfaces;

    internal abstract class Computer
    {
        private IMotherboard motherboard;
        private StorageProvider storageProvider;
        private Cpu cpu;

        public Computer(Cpu cpu, StorageProvider storageProvider, IMotherboard motherboard)
        {
            this.Cpu = cpu;
            this.Motherboard = motherboard;
            this.storageProvider = storageProvider;
        }

        protected StorageProvider StorageProvider
        {
            get
            {
                return this.storageProvider;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Storage provider cannot be null.");
                }

                this.storageProvider = value;
            }
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
                    throw new ArgumentNullException("Motherboard cannot be null.");
                }

                this.motherboard = value;
            }
        }

        protected virtual Cpu Cpu
        {
            get
            {
                return this.cpu;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CPU cannot be null.");
                }

                this.cpu = value;
            }
        }
    }
}