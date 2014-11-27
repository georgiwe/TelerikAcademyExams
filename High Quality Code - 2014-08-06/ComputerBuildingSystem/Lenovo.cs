namespace ComputersBuildingSystemCore
{
    using System.Collections.Generic;

    internal class Lenovo : ComputerManufacturer
    {
        public override PersonalComputer BuildPersonalComputer()
        {
            var ram = new Ram(4);
            var drawingStrat = new MonochromeDrawingStrategy();
            var videoCard = new VideoCard(false, drawingStrat);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu64(2, motherboard);
            var storage = new HardDrive(2000);

            var pc = new PersonalComputer(cpu, storage, motherboard);

            return pc;
        }

        public override Laptop BuildLaptopComputer()
        {
            var ram = new Ram(16);
            var drawingStrat = new ColorfulDrawingStrategy();
            var videoCard = new VideoCard(true, drawingStrat);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu64(2, motherboard);
            var storage = new HardDrive(1000);
            var battery = new LaptopBattery();

            var laptop = new Laptop(cpu, storage, motherboard, battery);

            return laptop;
        }

        public override Server BuildServerComputer()
        {
            var ram = new Ram(8);
            var drawingStrat = new MonochromeDrawingStrategy();
            var videoCard = new VideoCard(true, drawingStrat);
            var motherboard = new Motherboard(ram, videoCard);
            var cpu = new Cpu128(2, motherboard);
            var raidDrives = new List<StorageProvider>()
            {
                new HardDrive(500),
                new HardDrive(500)
            };

            var storage = new Raid(500, raidDrives);

            var server = new Server(cpu, storage, motherboard);

            return server;
        }
    }
}
