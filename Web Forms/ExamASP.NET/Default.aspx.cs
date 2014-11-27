﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ExamASP.NET.Models;

namespace ExamASP.NET
{
    public partial class _Default : Page
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Article> TopArticlesLV_GetData()
        {
            var top3 = this.db.Articles
                .OrderByDescending(a => a.Likes.Sum(l => l.Value))
                .Take(3);
            return top3;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Category> CategoriesLV_GetData()
        {
            return this.db.Categories;
        }
    }
}