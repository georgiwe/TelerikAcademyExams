namespace Cars.Import.JsonImport
{
    using Cars.Models;

    public class JsonItemPlaceholder
    {
        public int Year { get; set; }

        public TransmissionType Transmission { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public DealerInfoPlaceholder Dealer { get; set; }

        public string ManufacturerName { get; set; }
    }
}
