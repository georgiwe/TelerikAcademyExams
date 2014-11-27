namespace Exam.RESTApi.Models
{
    using System;
    using System.Linq.Expressions;

    using Exam.Models;

    public class OutputGameDataModelShort
    {
        public static Expression<Func<Game, OutputGameDataModelShort>> ToDataModel
        {
            get
            {
                return g => new OutputGameDataModelShort()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Blue = g.BluePlayer.UserName,
                    Red = g.RedPlayer.UserName,
                    GameState = g.GameState.ToString(),
                    DateCreated = g.DateCreated,
                };
            }
        }

        private string blue;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue
        {
            get
            {
                return this.blue == null ?
                    "No blue player yet" : this.blue;
            }

            set
            {
                this.blue = value;
            }
        }

        public string Red { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
