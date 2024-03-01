using System;
using System.Collections.Generic;

namespace Farm.Models
{
    public partial class User
    {
        public User()
        {
            Histories = new HashSet<History>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<History> Histories { get; set; }
    }
}
