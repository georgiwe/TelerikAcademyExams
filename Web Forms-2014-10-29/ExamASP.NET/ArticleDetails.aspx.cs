using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

using ExamASP.NET.Models;

namespace ExamASP.NET
{
    public partial class ArticleDetails : System.Web.UI.Page
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public object ArticleDetailsFV_GetItem([QueryString]int id)
        {
            return this.db.Articles.Include("Category").FirstOrDefault(a => a.Id == id);
        }
    }
}