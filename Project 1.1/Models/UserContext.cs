using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_1._1.Models
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("Connection")
        { }
        public DbSet<User> Users { get; set; }
    }
}