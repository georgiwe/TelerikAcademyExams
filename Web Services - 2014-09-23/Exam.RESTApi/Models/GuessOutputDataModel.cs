namespace Exam.RESTApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using Exam.Models;

    public class GuessOutputDataModel
    {
        public static Expression<Func<Guess, GuessOutputDataModel>> ToDataModel
        {
            get
            {
                return guess => new GuessOutputDataModel()
                    {
                        Id = guess.Id,
                        UserId = guess.UserId,
                        Username  = guess.User.UserName,
                        GameId = guess.GameId,
                        Number = guess.Number,
                        DateMade = guess.DateMade,
                        CowsCount = guess.CowsCount,
                        BullsCount = guess.BullsCount,
                    };
            }
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public int GameId { get; set; }

        [Required]
        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        public int BullsCount { get; set; }

        public int CowsCount { get; set; }
    }
}