using Project_1._1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_1._1.Models
{
    public class ArticleContext : DbContext
    {

        public ArticleContext() : base("Connection")
        { }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasMany(c => c.Articles)
                .WithMany(s => s.Tags)
                .Map(t => t.MapLeftKey("TagId")
                .MapRightKey("ArticleId")
                .ToTable("TagArticle"));
        }

    }
}