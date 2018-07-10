using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models
{
    public class Boarditem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Threaditem> Threads { get; set; }
    }
}