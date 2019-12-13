using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_1._1.Models
{
    public class User
    {
        public static object Identity { get; internal set; }
        public int Id { get; set; }

        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
    }
}