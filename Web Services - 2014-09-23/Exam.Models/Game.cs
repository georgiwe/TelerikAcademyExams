namespace Exam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        private HashSet<Guess> blueGuesses;
        private HashSet<Guess> redGuesses;

        public Game()
        {
            this.redGuesses = new HashSet<Guess>();
            this.blueGuesses = new HashSet<Guess>();
        }

        [Key]
        public int Id { get; set; }

        //[Required] // Possible foreign key problems
        public string RedPlayerId { get; set; }

        public string BluePlayerId { get; set; }

        public virtual User RedPlayer { get; set; }

        public virtual User BluePlayer { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ICollection<Guess> RedGuesses { get; set; }

        public virtual ICollection<Guess> BlueGuesses { get; set; }

        public GameState GameState { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string RedNumber { get; set; }

        [StringLength(4, MinimumLength = 4)]
        public string BlueNumber { get; set; }

        public string WinnerId { get; set; }

        public virtual User Winner { get; set; }
    }
}
