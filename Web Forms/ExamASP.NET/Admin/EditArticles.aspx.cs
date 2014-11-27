using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ExamASP.NET.Controls.ErrorSuccessNotifier;
using ExamASP.NET.Models;

namespace ExamASP.NET.Admin
{
    public partial class EditArticles : System.Web.UI.Page
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected const string ArticleDoesntExist = "This article was not found.";
        protected const string CouldNotUpdateArticleMsg = "Invalid Article data.";
        protected const string ArticleDeletedMsg = "Article Deleted.";
        protected const string ArticleCreatedMsg = "Article Created!";
        protected const string ArticleUpdatedMsg = "Article Updated!";
        protected const string CouldNotCreateArticleMsg = "Could not create article.";

        protected const string MustBeLoggedIn =
            "You must be a registered user and logged in, in order to create an article.";

        public IQueryable<Article> EditArticlesLV_GetData()
        {
            return this.db.Articles.Include("Category").Include("Likes").OrderBy(a => a.Id);
        }

        public void EditArticlesLV_UpdateItem(int id)
        {
            Article article = this.db.Articles.Find(id);
            if (article == null)
            {
                this.ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                ErrorSuccessNotifier.AddErrorMessage(ArticleDoesntExist);
                return;
            }

            this.TryUpdateModel(article);

            if (this.ModelState.IsValid)
            {
                this.db.SaveChanges();
                ErrorSuccessNotifier.AddSuccessMessage(ArticleUpdatedMsg);
            }
            else
            {
                ErrorSuccessNotifier.AddErrorMessage(CouldNotUpdateArticleMsg);
            }
        }

        public void EditArticlesLV_DeleteItem(int id)
        {
            var articleToDel = this.db.Articles.Find(id);

            if (articleToDel == null)
            {
                this.ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                ErrorSuccessNotifier.AddErrorMessage(ArticleDoesntExist);
                return;
            }

            this.db.Articles.Remove(articleToDel);
            this.db.SaveChanges();
            ErrorSuccessNotifier.AddSuccessMessage(ArticleDeletedMsg);
        }

        public IQueryable<Category> CategoriesDD_GetData()
        {
            return this.db.Categories;
        }

        protected void ChangeViewButt_Click(object sender, EventArgs e)
        {
            this.CreateArticleMultiView.SetActiveView(this.CreateArticleView);
        }

        protected void CancelCreateArticleButt_Click(object sender, EventArgs e)
        {
            foreach (var ctrl in this.CreateArticleView.Controls)
            {
                var tb = ctrl as TextBox;

                if (tb != null)
                {
                    tb.Text = string.Empty;
                }
            }

            this.CreateArticleMultiView.SetActiveView(this.CreateBtnView);
        }

        public void CreateArticleFV_InsertItem()
        {
            var newArticle = new Article();
            this.TryUpdateModel(newArticle);

            if (this.ModelState.IsValid == false)
            {
                ErrorSuccessNotifier.AddErrorMessage(CouldNotCreateArticleMsg);
                return;
            }

            var currUserName = this.User.Identity.Name;
            var currUser = this.db.Users.FirstOrDefault(u => u.UserName == currUserName);

            if (currUser == null)
            {
                ErrorSuccessNotifier.AddErrorMessage(MustBeLoggedIn);
                return;
            }

            var newArticleLikes = new Like() { Value = 0 };
            newArticle.DateCreated = DateTime.Now;
            newArticle.Author = currUser;

            this.db.Articles.Add(newArticle);
            this.db.SaveChanges();
            ErrorSuccessNotifier.AddSuccessMessage(ArticleCreatedMsg);
            this.CancelCreateArticleButt_Click(new object(), EventArgs.Empty);
        }

        protected void SortByCategoryName(object sender, EventArgs e)
        {
        }

        protected void SortByLikesCount(object sender, EventArgs e)
        {
        }
    }
}