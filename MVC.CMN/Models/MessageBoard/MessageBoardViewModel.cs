using System.Collections.Generic;

namespace MVC.CMN.Models.MessageBoard {

    public class MessageBoardViewModel {
        public IList<Board> Boarditems { get; set; }

        public MessageBoardViewModel() {
            Boarditems = new List<Board>();
        }
    }
}