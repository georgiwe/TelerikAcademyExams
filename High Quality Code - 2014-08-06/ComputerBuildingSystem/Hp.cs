namespace ComputersBuildingSystemCore
{
    using System.Collections.Generic;

    internal class Hp : ComputerManufacturer
    {
        public override PersonalComputer BuildPersonalComputer()
        {
            // TODO: Implement builder pattern
            var ram = new Ram(2);
            var colorfulVideoCardStrategy = new ColorfulDrawingStrategy();
            var videoCard = new VideoCard(true, colorfulVideoCardStrategy);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu32(2, motherboard);
            var storage = new HardDrive(500);

            var pc = new PersonalComputer(cpu, storage, motherboard);

            return pc;
        }

        public override Laptop BuildLaptopComputer()
        {
            var ram = new Ram(4);
            var colorfulVideoCardStrategy = new ColorfulDrawingStrategy();
            var videoCard = new VideoCard(true, colorfulVideoCardStrategy);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu64(2, motherboard);
            var storage = new HardDrive(500);
            var battery = new LaptopBattery();

            var laptop = new Laptop(cpu, storage, motherboard, battery);

            return laptop;
        }

        public override Server BuildServerComputer()
        {
            var ram = new Ram(32);
            var monochromStrat = new MonochromeDrawingStrategy();
            var videoCard = new VideoCard(false, monochromStrat);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu32(4, motherboard);
            var raidDrives = new List<StorageProvider>()
            {
                new HardDrive(1000),
                new HardDrive(1000)
            };

            var raid = new Raid(1000, raidDrives);

            var server = new Server(cpu, raid, motherboard);

            return server;
        }
    }
}
