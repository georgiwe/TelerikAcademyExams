namespace Cars.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        public int CarId { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public virtual Dealer Dealer { get; set; }

        [Required]
        public TransmissionType Transmission { get; set; }

        [Required]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
