namespace ComputersBuildingSystemCore.Exceptions
{
    using System;

    public class InvalidManufacturerException : ArgumentException
    {
        public InvalidManufacturerException(string message) : base(message)
        {
        }
    }
}