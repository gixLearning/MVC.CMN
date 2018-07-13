using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard
{
    public class QuotePostViewModel
    {
        public int ThreadId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}