using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models.MessageBoard {
    [Table("UserProfiles")]
    public class UserProfile {
        public int PostCount { get; set; }
        public int Upvotes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }

        [Key]
        public string UserId { get; set; }

        public UserProfile() {
            Posts = new HashSet<Post>();
            Threads = new HashSet<Thread>();
        }
    }
}