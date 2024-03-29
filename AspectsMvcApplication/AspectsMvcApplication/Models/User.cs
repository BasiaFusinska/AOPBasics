﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspectsMvcApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

        public IEnumerable<Game> BoughtGames { get; set; } 
    }
}