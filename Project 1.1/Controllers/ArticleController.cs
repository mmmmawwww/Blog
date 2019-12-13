using Project_1._1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Project_1._1.Controllers
{
    public class ArticleController : Controller
    {
        ArticleContext db = new ArticleContext();
        // GET: Article
        public ActionResult Index(string category, string date, string selectedTags, int? page)
         {
             Session["category"] = category;
             Session["date"] = date;
                Session["selectedTags"] = selectedTags;
            string[] arrayTag = null;
            if (selectedTags != null)
            {
                 arrayTag = selectedTags.Split('#');
            }
            IEnumerable<Article> article = ArticleFilter.Filter(category, date, arrayTag);
            var categorys = db.Categorys;
            List<string> categoryList = new List<string>();
            categoryList.Add("Все");
            foreach (var c in categorys)
            {
                categoryList.Add(c.Name);
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            article = article.OrderByDescending(s => s.Date);
            ArticlesListViewModel ivm = new ArticlesListViewModel
            {
                Articles = article.ToList(),
                Categorys = new SelectList(categoryList),
                Date = date,
                PagedArticle = article.ToList().ToPagedList(pageNumber, pageSize)
          };

            return View(ivm);
        }
        public ActionResult Article(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            List<Article> listarticle = db.Articles.Include(t=>t.Category).ToList();
            Article article = listarticle.Find(t => t.Id == id);
            if (article != null)
            {
                return View(article);
            }
            return HttpNotFound();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}