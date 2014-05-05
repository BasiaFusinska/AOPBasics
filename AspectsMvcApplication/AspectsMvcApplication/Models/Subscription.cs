using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspectsMvcApplication.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}