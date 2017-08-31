using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthStudy.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<GroupAccess> GroupAccess { get; set; }
        public List<UserAccess> UserAccess { get; set; }
    }
}
