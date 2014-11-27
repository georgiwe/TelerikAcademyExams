namespace ComputersBuildingSystemCore
{
    internal abstract class ComputerManufacturer
    {
        public abstract PersonalComputer BuildPersonalComputer();

        public abstract Laptop BuildLaptopComputer();

        public abstract Server BuildServerComputer();
    }
}
