using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard
{
    public class EditThreadViewModel
    {
        public int BoardId { get; set; }
        public int ThreadId { get; set; }
        public string UserId { get; set; }
        public string Subject { get; set; }
    }
}