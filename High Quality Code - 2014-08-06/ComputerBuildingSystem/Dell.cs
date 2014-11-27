namespace ComputersBuildingSystemCore
{
    using System.Collections.Generic;

    internal class Dell : ComputerManufacturer
    {
        private const int DefaultPcCpuCores = 4;
        private const int DefaultPcRamAmount = 8;
        private const int DefaultPcHardDriveSize = 1000;

        private const int DefaultLaptopRamAmount = 8;
        private const int DefaultLaptopCpuCores = 4;
        private const int DefaultLaptopHardDriveSize = 1000;

        private const int DefaultServerRamAmount = 64;
        private const int DefaultServerCpuCores = 8;
        private const int DefaultServerHardDriveSize = 2000;

        public override PersonalComputer BuildPersonalComputer()
        {
            // TODO: Implement builder pattern
            var ram = new Ram(DefaultPcRamAmount);
            var colorfulVideoCardStrategy = new ColorfulDrawingStrategy();
            var videoCard = new VideoCard(true, colorfulVideoCardStrategy);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu64(DefaultPcCpuCores, motherboard);
            var storage = new HardDrive(DefaultPcHardDriveSize);

            var pc = new PersonalComputer(cpu, storage, motherboard);

            return pc;
        }

        public override Laptop BuildLaptopComputer()
        {
            var ram = new Ram(DefaultLaptopRamAmount);
            var colorfulVideoCardStrategy = new ColorfulDrawingStrategy();
            var videoCard = new VideoCard(true, colorfulVideoCardStrategy);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu32(DefaultLaptopCpuCores, motherboard);
            var storage = new HardDrive(DefaultLaptopHardDriveSize);
            var battery = new LaptopBattery();

            var laptop = new Laptop(cpu, storage, motherboard, battery);

            return laptop;
        }

        public override Server BuildServerComputer()
        {
            var ram = new Ram(DefaultServerRamAmount);
            var monochromStrat = new MonochromeDrawingStrategy();
            var videoCard = new VideoCard(false, monochromStrat);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu64(DefaultServerCpuCores, motherboard);
            var raidDrives = new List<StorageProvider>()
            {
                new HardDrive(DefaultServerHardDriveSize),
                new HardDrive(DefaultServerHardDriveSize)
            };

            var raid = new Raid(DefaultServerHardDriveSize, raidDrives);

            var server = new Server(cpu, raid, motherboard);

            return server;
        }
    }
}
