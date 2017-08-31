using System.Collections.Generic;

namespace AuthStudy.DAL.Models
{
    public class UserAccess
    {
        public int UserId { get; set; }
        public int AccessId { get; set; }

        public Access Access { get; set; }
    }
}