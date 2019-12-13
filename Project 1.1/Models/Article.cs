using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_1._1.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public Article()
        {
            Tags = new List<Tag>();
        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
        public Category()
        {
            Articles = new List<Article>();
        }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public Tag()
        {
            Articles = new List<Article>();
        }
    }


    public class ArticlesListViewModel
    {
        public List<Article> Articles { get; set; }
        public SelectList Categorys { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }
        public PagedList.IPagedList<Article> PagedArticle { get; set; }
    }
}

