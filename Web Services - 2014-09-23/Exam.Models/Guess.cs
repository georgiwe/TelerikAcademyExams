namespace Exam.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Guess
    {
        [Key]
        public int Id { get; set; }

        [Required] // Possible foreign key roblems?
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}
