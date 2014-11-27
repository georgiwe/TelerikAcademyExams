namespace ComputersBuildingSystemCore
{
    using System;

    internal class EntryPoint
    {
        public static void Main()
        {
            var manufacturerName = Console.ReadLine();
            manufacturerName = manufacturerName.ToLowerInvariant();
            var manufacturer = SimpleManufacturerFactory.GetManufacturer(manufacturerName);

            var personalComputer = manufacturer.BuildPersonalComputer();
            var laptop = manufacturer.BuildLaptopComputer();
            var server = manufacturer.BuildServerComputer();

            while (true)
            {
                var command = Console.ReadLine();

                if (command == null ||
                    command.StartsWith("Exit"))
                {
                    break;
                }

                var commandParts = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (commandParts.Length != 2)
                {
                    Console.WriteLine("Invalid command!");
                    continue;
                }

                var commandName = commandParts[0];
                var commandArgument = int.Parse(commandParts[1]);

                if (commandName == "Charge")
                {
                    laptop.ChargeBattery(commandArgument);
                }
                else if (commandName == "Process")
                {
                    server.ProcessRequest(commandArgument);
                }
                else if (commandName == "Play")
                {
                    personalComputer.PlayGame(commandArgument);
                }
                else
                {
                    Console.WriteLine("Invalid command!");
                }
            }
        }
    }
}