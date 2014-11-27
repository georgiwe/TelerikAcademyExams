namespace Exam.RESTApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using Exam.Models;

    public class GameCreationDataModel
    {
        public GameCreationDataModel()
        {
            this.Blue = "No blue player yet";
            this.GameState = 
                Exam.Models.GameState.WaitingForOpponent.ToString();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        public string Blue { get; set; }

        public string Red { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}