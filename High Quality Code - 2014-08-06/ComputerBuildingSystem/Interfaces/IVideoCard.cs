namespace ComputersBuildingSystemCore.Interfaces
{
    public interface IVideoCard
    {
        bool IsColorful { get; }

        void Draw(string data);
    }
}
