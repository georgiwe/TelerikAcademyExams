namespace ComputersBuildingSystemCore
{
    using System;

    using ComputersBuildingSystemCore.Interfaces;

    internal class VideoCard : IVideoCard
    {
        private IDrawingStrategy drawingStrategy;

        public VideoCard(bool isColorful, IDrawingStrategy drawingStrategy)
        {
            this.IsColorful = isColorful;
            this.DrawingStrategy = drawingStrategy;
        }

        public bool IsColorful { get; set; }

        protected IDrawingStrategy DrawingStrategy
        {
            get
            {
                return this.drawingStrategy;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Drawing strategy cannot be null.");
                }

                this.drawingStrategy = value;
            }
        }

        public void Draw(string data)
        {
            this.DrawingStrategy.Draw(data);
        }
    }
}
