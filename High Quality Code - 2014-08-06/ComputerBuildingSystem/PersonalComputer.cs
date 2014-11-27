namespace ComputersBuildingSystemCore
{
    using ComputersBuildingSystemCore.Interfaces;

    internal class PersonalComputer : Computer
    {
        public PersonalComputer(Cpu cpu, StorageProvider storageProvider, IMotherboard motherboard)
            : base(cpu, storageProvider, motherboard)
        {
        }

        public void PlayGame(int guessNumber)
        {
            this.Cpu.GenerateRandomNumber(1, 10);
            var number = this.Motherboard.LoadRamValue();
            var gameResult = "You win!";

            if (number != guessNumber)
            {
                gameResult = string.Format("You didn't guess the number {0}.", number);
            }

            this.Motherboard.DrawOnVideoCard(gameResult);
        }
    }
}
