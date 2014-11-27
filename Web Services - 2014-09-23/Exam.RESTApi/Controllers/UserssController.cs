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

    public class UserssController : BaseApiController
    {
        public UserssController(IBullsAndCowsData data)
            : base(data)
        {
        }

    }
}