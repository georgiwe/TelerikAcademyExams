namespace Exam.RESTApi.Models
{
    using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

    public class GameOutputModelDetailed
    {
        public static Expression<Func<Game, GameOutputModelDetailed>> ToDetailedModel
        {
            get
            {
                return game => new GameOutputModelDetailed()
                {
                    Id = game.Id,
                    Name = game.Name,
                    DateCreated = game.DateCreated,
                    Red = game.RedPlayer.UserName,
                    Blue = game.BluePlayer.UserName,
                    GameState = game.GameState.ToString(),
                    // your number
                    // your guesses
                    // your color
                };
            }
        }

        private string yourColor;

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public string YourNumber { get; set; }

        public ICollection<GuessOutputDataModel> YourGuesses { get; set; }

        public string YourColor
        {
            get
            {
                return this.yourColor;
            }

            set
            {
                this.yourColor = value;
            }
        }
        public string GameState { get; set; }
    }
}