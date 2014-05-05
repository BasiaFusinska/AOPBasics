using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspectsMvcApplication.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
    }
}