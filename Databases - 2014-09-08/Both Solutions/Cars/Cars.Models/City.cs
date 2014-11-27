namespace Cars.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class City
    {
        public int CityID { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(11)]
        public string Name { get; set; }
    }
}
