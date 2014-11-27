using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ExamASP.NET.Controls.ErrorSuccessNotifier;
using ExamASP.NET.Models;

namespace ExamASP.NET.Controls.LikesControl
{
    public partial class LikesControl : System.Web.UI.UserControl
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        private const string IdMissingMsg = "Id from URL is missing.";
        private const string ArticleNotFounndMsg = "Article was not found.";
        private const string AlreadyVotedMsg = 
            "You cannot vote the same way, for the same article, more than once.";

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var article = this.GetArticleFromUrlId();

            if (article == null) return;

            this.VoteDisplayLb.Text = article.Likes.Sum(l => l.Value).ToString();
        }

        protected Article GetArticleFromUrlId()
        {
            var idStr = this.Request.Params["id"];
            if (idStr == null)
            {
                ErrorSuccessNotifier.ErrorSuccessNotifier.AddErrorMessage(IdMissingMsg);
                return null;
            }

            var id = int.Parse(idStr);
            var article = this.db.Articles.Find(id);

            if (article == null)
            {
                ErrorSuccessNotifier.ErrorSuccessNotifier.AddErrorMessage(ArticleNotFounndMsg);
                return null;
            }

            return article;
        }

        protected void ProcessVote(object sender, EventArgs e)
        {
            var article = this.GetArticleFromUrlId();

            if (article == null) return;

            var currUserName = HttpContext.Current.User.Identity.Name;
            var usersVote = article.Likes.FirstOrDefault(l => l.User.UserName == currUserName);
            //var userHasVoted = article.Likes.Any(l => l.ByUser.UserName == currUserName);

            var senderBtn = (Button)sender;
            var isUpvote = senderBtn.CommandName == "Up";

            // User has voted for this article before
            if (usersVote != null && article.Likes.Any())
            {
                if (usersVote.Value == 1 && isUpvote ||
                    usersVote.Value == -1 && isUpvote == false)
                {
                    ErrorSuccessNotifier.ErrorSuccessNotifier.AddErrorMessage(AlreadyVotedMsg);
                    return;
                }

                usersVote.Value += isUpvote ? 1 : -1;

                this.db.SaveChanges();
                return;
            }

            // User has not voted for this article before
            var currUser = this.db.Users.FirstOrDefault(u => u.UserName == currUserName);
            var newLike = new Like();
            newLike.Value = isUpvote ? 1 : -1;
            newLike.User = currUser;

            article.Likes.Add(newLike);
            //this.db.Likes.Add(newLike);
            this.db.SaveChanges();
        }
    }
}