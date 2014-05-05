using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspectsMvcApplication.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public bool IsPremium { get; set; }
        public bool IsBought { get; set; }
    }
}