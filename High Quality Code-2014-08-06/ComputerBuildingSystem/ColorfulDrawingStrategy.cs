namespace ComputersBuildingSystemCore
{
    using System;

    using ComputersBuildingSystemCore.Interfaces;

    internal class ColorfulDrawingStrategy : IDrawingStrategy
    {
        public void Draw(string data)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(data);
            Console.ResetColor();
        }
    }
}
