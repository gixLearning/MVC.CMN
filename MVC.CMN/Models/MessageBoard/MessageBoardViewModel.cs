using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard {
    public class MessageBoardViewModel {
        public IList<Board> Boarditems { get; set; }

        public MessageBoardViewModel() {
            Boarditems = new List<Board>();
        }
    }
}