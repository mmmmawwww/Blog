using PagedList;
using Project_1._1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


namespace Project_1._1.Controllers
{

    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        ArticleContext db = new ArticleContext();
        public ActionResult AdminPanel(string category, string date, string selectedTags, int? page)
        {
          if (User.Identity.IsAuthenticated)
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
           return RedirectToAction("Login", "Auth");
        }
        [Authorize]
        public ActionResult Registration()
        {
            return RedirectToAction("Register", "Auth");
        }
        public ActionResult Tags(int? page)
        {
            var Tags = db.Tags.OrderBy(s => s.Name);
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(Tags.ToPagedList(pageNumber,pageSize));
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddTag()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddTag(Tag tag)
        {
            if (tag == null)
            {
                return RedirectToAction("Tags");
            }
            db.Tags.Add(tag);
            db.SaveChanges();
            return RedirectToAction("Tags");
        }
        [Authorize]
        [HttpGet]
        public ActionResult EditTag(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Tags");
            }
            Tag tags = db.Tags.Find(id);
            if (tags != null)
            {
                return View(tags);
            }
            return HttpNotFound();
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditTag(Tag tag)
        {
            if (tag == null)
            {
                return RedirectToAction("Tags");
            }
            db.Entry(tag).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Tags");
        }
        [Authorize]
        [HttpGet]
        public ActionResult DeleteTag(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Tags");
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return RedirectToAction("Tags");
            }
            return View(tag);
        }
        [Authorize]
        [HttpPost, ActionName("DeleteTag")]
        public ActionResult DeleteTagConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Tags");
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return RedirectToAction("Tags");
            }
            db.Tags.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("Tags");
        }
        [Authorize]
        public ActionResult Category(int? page)
        {
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            var Categorys = db.Categorys.OrderByDescending(s => s.Name); 
            return View(Categorys.ToPagedList(pageNumber,pageSize));
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (category == null)
            {
                return RedirectToAction("Category");
            }
            db.Categorys.Add(category);
            db.SaveChanges();
            return RedirectToAction("Category");
        }
        [Authorize]
        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Category");
            }
            Category category = db.Categorys.Find(id);
            if (category != null)
            {
                return View(category);
            }
            return HttpNotFound();
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (category == null)
            {
                return RedirectToAction("Category");
            }
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Category");
        }
        [Authorize]
        [HttpGet]
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Category");
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return RedirectToAction("Category");
            }
            return View(category);
        }
        [Authorize]
        [HttpPost, ActionName("DeleteCategory")]
        public ActionResult DeleteCategoryConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Category");
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return RedirectToAction("Category");
            }
            db.Categorys.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Tags");
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddArticle()
        {
            SelectList categorysList = new SelectList(db.Categorys, "Id", "Name");
            SelectList tagsList = new SelectList(db.Tags, "Id", "Name");
            ViewBag.Tags = tagsList;
            ViewBag.Categorys = categorysList;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddArticle(Article article, int[] selectedTags)
        {
            if (article == null)
            {
                return RedirectToAction("AdminPanel");
            }
            article.Date = DateTime.Now;

            if (selectedTags != null)
            {
                //получаем выбранные курсы
                foreach (var c in db.Tags.Where(co => selectedTags.Contains(co.Id)))
                {
                    article.Tags.Add(c);
                }
            }
            db.Articles.Add(article);
            db.SaveChanges();
            return RedirectToAction("AdminPanel");          
        }
        [Authorize]
        [HttpGet]
        public ActionResult EditArticle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AdminPanel");
            }
            Article article = db.Articles.Find(id);
            if (article != null)
            {
                SelectList categorysList = new SelectList(db.Categorys, "Id", "Name");
                SelectList tagsList = new SelectList(db.Tags, "Id", "Name");
                ViewBag.Tags = db.Tags;
                ViewBag.Categorys = categorysList;
                return View(article);
            }
            return RedirectToAction("AdminPanel");
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditArticle(Article article, List<string> selectedTags )
        {
            if (article == null)
            {
                return RedirectToAction("AdminPanel");
            }
            Article newArticle = new Article();
            newArticle.Name = article.Name;
            newArticle.Description = article.Description;
            newArticle.ShortDescription = article.ShortDescription;
            newArticle.CategoryId = article.CategoryId;
            newArticle.Category = article.Category;
            newArticle.Date = article.Date;

            if (selectedTags != null)
            {
                foreach (var c in db.Tags.Where(co => selectedTags.Contains(co.Id.ToString())))
                {
                    newArticle.Tags.Add(c);
                }
            }
            Article art = db.Articles.Find(article.Id);
            db.Articles.Remove(art);
            db.Articles.Add(newArticle);
            db.SaveChanges();
            return RedirectToAction("AdminPanel");
        }
        [Authorize]
        [HttpGet]
        public ActionResult DeleteArticle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AdminPanel");
            }
            else
            {
                List<Article> listarticle = db.Articles.Include(t => t.Category).ToList();
                Article art = listarticle.Find(t => t.Id == id);

                if (art == null)
                {
                    return RedirectToAction("AdminPanel");
                }
                ViewBag.Tags = db.Tags.ToList();
                return View(art);
            }

        }
        [Authorize]
        [HttpPost, ActionName("DeleteArticle")]
        public ActionResult DeleteArticleConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AdminPanel");
            }
            else
            {
                Article art = db.Articles.Find(id);
                if (art == null)
                {
                    return RedirectToAction("AdminPanel");
                }
                db.Articles.Remove(art);
                db.SaveChanges();
                return RedirectToAction("AdminPanel");
            }

        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Auth");
        }
        protected override void Initialize(RequestContext requestContext)
        {
            // no client input will be checked on any controllers
            ValidateRequest = false;
            base.Initialize(requestContext);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}