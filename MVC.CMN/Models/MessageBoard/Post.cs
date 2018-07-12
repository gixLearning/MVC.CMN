using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.CMN.Models.MessageBoard {

    [Table("Posts")]
    public class Post {
        public int PostId { get; set; }

        [Required]
        public int ThreadId { get; set; }
        public DateTime Created { get; set; }

        public virtual Thread Thread { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        [Required]
        [StringLength(128)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }
    }
}