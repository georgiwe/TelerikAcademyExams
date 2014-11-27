namespace Exam.RESTApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Exam.Data;
    using Exam.Models;
    using Exam.RESTApi.Models;
    using System.Web;

    public class ScoresController : BaseApiController
    {
        public ScoresController(IBullsAndCowsData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult GetLeaderborad()
        {
            var result = this.data.Users.All()
                .Select(u => new
                {
                    Username = u.UserName,
                    Rank = 100 * u.WinsCount + 15 * u.LossesCount
                })
                .OrderByDescending(u => u.Rank)
                .ThenBy(u => u.Username);

            return this.Ok(result);
        }
    }
}