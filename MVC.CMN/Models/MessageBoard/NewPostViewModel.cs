using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard
{
    public class NewPostViewModel
    {
        public int ThreadId { get; set; }
        public string UserId { get; set; }
    }
}