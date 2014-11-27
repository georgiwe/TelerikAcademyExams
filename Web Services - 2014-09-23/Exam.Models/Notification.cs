namespace Exam.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public bool State { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
