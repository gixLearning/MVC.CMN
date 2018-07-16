using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard
{
    public class ShowThreadViewModel
    {
        public Thread theThread { get; set; }
        public int index { get; set; }
    }
}