namespace ComputersBuildingSystemCore.Interfaces
{
    /// <summary>
    /// Interface that combines the segregated interfaces
    /// IRamOperator and IVideocardOperator.
    /// It handles operations between the CPU, the VideoCard and the RAM.
    /// It saves and loads data to the Ram, when asked by the CPU.
    /// It prints to the video card when asked by the CPU.
    /// It is a mediator between the three. It decouples them,
    /// as the CPU, the graphics card and teh RAM only know about
    /// the Motherboard.
    /// </summary>
    internal interface IMotherboard
    {
        /// <summary>
        /// Saves an integer value inside the RAM, associated with the motherboard.
        /// </summary>
        /// <param name="newValue">The value that needs to be saved in the RAM.</param>
        void SaveRamValue(int newValue);

        /// <summary>
        /// Loads a previously saved value from the RAM, associated with the motherboard.
        /// </summary>
        /// <returns>Returns the value, read from the RAM, associated with the motherboard.</returns>
        int LoadRamValue();

        /// <summary>
        /// Uses the VideoCard object, associated with the motherboard
        /// to output data to the outputstream it is set to.
        /// </summary>
        /// <param name="data">The output data to be sent to the output stream.</param>
        void DrawOnVideoCard(string data);
    }
}