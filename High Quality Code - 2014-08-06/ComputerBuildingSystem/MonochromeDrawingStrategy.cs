namespace ComputersBuildingSystemCore
{
    using System;

    using ComputersBuildingSystemCore.Interfaces;

    internal class MonochromeDrawingStrategy : IDrawingStrategy
    {
        public void Draw(string data)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(data);
            Console.ResetColor();
        }
    }
}
