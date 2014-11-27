namespace Exam.RESTApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Exam.Data;
    using Exam.Models;
    using Exam.RESTApi.Models;

    [Authorize]
    public class NotificationsController : BaseApiController
    {
        public NotificationsController(IBullsAndCowsData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var currUserId = this.User.Identity.GetUserId();
            var notifications = this.GetOrderedNotificationsForUser(currUserId);

            return this.Ok(notifications);
        }

        [HttpGet]
        public IHttpActionResult GetPaged(int page)
        {
            var userId = this.User.Identity.GetUserId();
            var paged = this.GetOrderedNotificationsForUser(userId)
                .Skip(page * 10)
                .Take(10);

            return this.Ok(paged);
        }

        [HttpGet]
        public IHttpActionResult GetNext()
        {
            var userId = this.User.Identity.GetUserId();
            var oldestDate = this.data.Notifications.All()
                .Min(n => n.DateCreated);

            var oldestNotif = this.data.Notifications.All().FirstOrDefault(n => n.DateCreated == oldestDate);

            return this.Ok(oldestNotif);
        }

        private IQueryable<NotificationOutputDataModel> GetOrderedNotificationsForUser(string userId)
        {
            var notifications = this.data.Notifications.All()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.DateCreated)
                .Select(NotificationOutputDataModel.ToModel);

            return notifications;
        }
    }
}