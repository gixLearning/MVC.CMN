using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard
{
    public class EditBoardViewModel
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}