using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models
{
    public class Postitem
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public Threaditem BelongsToThread { get; set; }

    }
}