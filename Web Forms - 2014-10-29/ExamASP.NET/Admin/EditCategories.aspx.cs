using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ExamASP.NET.Controls.ErrorSuccessNotifier;
using ExamASP.NET.Models;

namespace ExamASP.NET.Admin
{
    public partial class EditCategories : Page
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected const string CategoryDoesntExistMsg = "This category was not found.";
        protected const string CouldNotSaveChangesMsg = "Could not save changes.";
        protected const string CouldNotAddCategoryMsg = "Could not add new category. Make sure your data is correct.";
        protected const string CategoryNameExistsMsg = "This category already exists.";
        protected const string CategoryUpdatedMsg = "Category Updated!";
        protected const string CategoryDeletedMsg = "Category Deleted!";
        protected const string CategoryCreatedMsg = "Category Created!";

        public IQueryable<Category> EditCategoryLV_GetData()
        {
            return this.db.Categories.OrderBy(c => c.Id);
        }

        public void EditCategoryLV_UpdateItem(int id)
        {
            Category category = this.db.Categories.Find(id);
            var oldCatName = category.Name; // old cat - lol

            if (category == null)
            {
                this.ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                ErrorSuccessNotifier.AddErrorMessage(CategoryDoesntExistMsg);
                return;
            }

            this.TryUpdateModel(category);

            if (this.ModelState.IsValid == false)
            {
                ErrorSuccessNotifier.AddErrorMessage(CouldNotSaveChangesMsg);
                return;
            }

            if (this.db.Categories.Any(c => c.Name == category.Name) && 
                category.Name != oldCatName)
            {
                ErrorSuccessNotifier.AddErrorMessage(CategoryNameExistsMsg);
                return;
            }

            this.db.SaveChanges();
            ErrorSuccessNotifier.AddSuccessMessage(CategoryUpdatedMsg);
        }

        public void EditCategoryLV_DeleteItem(int id)
        {
            var catToDel = this.db.Categories.Find(id);

            if (catToDel == null)
            {
                ErrorSuccessNotifier.AddErrorMessage(CategoryDoesntExistMsg);
                return;
            }

            var articlesToRemove = new List<Article>();
            foreach (var article in catToDel.Articles)
            {
                articlesToRemove.Add(article);
            }

            foreach (var article in articlesToRemove)
            {
                this.db.Articles.Remove(article);
            }

            this.db.Categories.Remove(catToDel);
            this.db.SaveChanges();
            ErrorSuccessNotifier.AddSuccessMessage(CategoryDeletedMsg);
        }

        public void EditCategoryLV_InsertItem()
        {
            var newCategory = new Category();

            this.TryUpdateModel(newCategory);

            if (this.ModelState.IsValid == false)
            {
                ErrorSuccessNotifier.AddErrorMessage(CouldNotAddCategoryMsg);
                return;
            }

            if (this.db.Categories.Any(c => c.Name == newCategory.Name))
            {
                ErrorSuccessNotifier.AddErrorMessage(CategoryNameExistsMsg);
                return;
            }

            this.db.Categories.Add(newCategory);
            this.db.SaveChanges();
            ErrorSuccessNotifier.AddSuccessMessage(CategoryCreatedMsg);
        }
    }
}