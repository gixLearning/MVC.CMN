﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models
{
    public class Newsitem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Datestamp { get; set; }
        public string Colorstyle { get; set; }

        //This will be replaced with the actual database later

    }
}