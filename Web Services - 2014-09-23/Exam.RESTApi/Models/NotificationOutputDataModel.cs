namespace Exam.RESTApi.Models
{
    using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

    public class NotificationOutputDataModel
    {
        private string state;

        public static Expression<Func<Notification, NotificationOutputDataModel>> ToModel
        {
            get
            {
                return n => new NotificationOutputDataModel()
                {
                    Id = n.Id,
                    DateCreated = n.DateCreated,
                    GameId = n.GameId,
                    Message = n.Message,
                    Type = "YourTurn", // this is default, will be changed elsewhere
                    State = n.State.ToString(),
                };
            }
        }

        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public string Type { get; set; }

        public string State
        {
            get
            {
                if (this.state == "true")
                {
                    return "Read";
                }

                return "Unread";
            }

            set
            {
                this.state = value;
            }
        }

        public int GameId { get; set; }
    }
}