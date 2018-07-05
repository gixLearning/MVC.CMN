﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard {
    [Table("Boards")]
    public class Board {        
        public int BoardId { get; set; }        
        public int ThreadCount { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

    }
}