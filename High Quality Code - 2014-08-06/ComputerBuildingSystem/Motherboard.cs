namespace ComputersBuildingSystemCore
{
    using System;

    using ComputersBuildingSystemCore.Interfaces;

    public class Motherboard : IMotherboard
    {
        private IRam ram;
        private IVideoCard videoCard;

        public Motherboard(IRam ram, IVideoCard videoCard)
        {
            this.Ram = ram;
            this.VideoCard = videoCard;
        }

        // Note these are PRIVATE properties
        // For validation purposes only
        private IRam Ram 
        {
            get
            {
                return this.ram;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Passed Ram cannot be null.");
                }

                this.ram = value;
            }
        }

        private IVideoCard VideoCard
        {
            get
            {
                return this.videoCard;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Passed VideoCard cannot be null.");
                }

                this.videoCard = value;
            }
        }

        public int LoadRamValue()
        {
            var loadedValue = this.Ram.LoadValue();
            return loadedValue;
        }

        public void SaveRamValue(int value)
        {
            this.Ram.SaveValue(value);
        }

        public void DrawOnVideoCard(string data)
        {
            this.VideoCard.Draw(data);
        }
    }
}
