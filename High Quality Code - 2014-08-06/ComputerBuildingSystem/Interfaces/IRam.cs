namespace ComputersBuildingSystemCore.Interfaces
{
    public interface IRam
    {
        void SaveValue(int newValue);

        int LoadValue();
    }
}
