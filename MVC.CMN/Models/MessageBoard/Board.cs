using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.CMN.Models.MessageBoard {

    [Table("Boards")]
    public class Board {
        public int BoardId { get; set; }

        //Probably don't need this since we can get it by counting items in the threads-list. =P
        public int ThreadCount { get; set; }

        public virtual ICollection<Thread> Threads { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public Board()
        {
            Threads = new HashSet<Thread>();
        }
    }
}