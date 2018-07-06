using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard {
    [Table("Threads")]
    public class Thread {
        public int ThreadId { get; set; }
        public int BoardId { get; set; }
        public int PostCount { get; set; }
        public DateTime Created { get; set; }

        public virtual Board Board { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(128)]
        public string Subject { get; set; }

        public Thread() {
            Posts = new HashSet<Post>();
        }
    }
}