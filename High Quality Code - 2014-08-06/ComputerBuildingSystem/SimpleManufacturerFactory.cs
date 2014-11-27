namespace ComputersBuildingSystemCore
{
    using System;

    using ComputersBuildingSystemCore.Exceptions;

    internal static class SimpleManufacturerFactory
    {
        internal static ComputerManufacturer GetManufacturer(string manufacturerName)
        {
            ComputerManufacturer manufacturer = null;

            switch (manufacturerName)
            {
                case "hp":
                    manufacturer = new Hp();
                    break;

                case "dell":
                    manufacturer = new Dell();
                    break;

                case "lenovo":
                    manufacturer = new Lenovo();
                    break;

                default:
                    throw new InvalidManufacturerException("Invalid manufacturer!");
            }

            return manufacturer;
        }
    }
}
