using Project_1._1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_1._1.Controllers
{
    public class ArticleFilter
    {
        
        public static IEnumerable<Article> Filter(string category, string date, string[] selectedTags)
        {
            ArticleContext db = new ArticleContext();
            IEnumerable<Article> article = db.Articles.Include(t => t.Category);

            if (!String.IsNullOrEmpty(category) && !category.Equals("Все")) // фильтрация по категории
            {
                article = article.Where(p => p.Category.Name == category);
            }
            if (!String.IsNullOrEmpty(date)) // фильтрация по дате
            {
                article = article.Where(p => p.Date.ToString("yyyy-MM-dd") == date);
            }
            List<Tag> tagList = db.Tags.ToList();
            if (selectedTags != null)
            {

                List<Tag> temp = new List<Tag>();
                foreach (Tag c in tagList)
                {
                    if (selectedTags.Contains(c.Name))
                    {
                        temp.Add(c);
                    }
                }
                foreach (Tag c in temp)
                {
                    article = article.Where(p => p.Tags.Contains(c));
                }
            }
            return article;
        }
    }
}