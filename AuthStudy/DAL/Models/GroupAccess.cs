using System.Collections.Generic;

namespace AuthStudy.DAL.Models
{
    public class GroupAccess
    {
        public int GroupId { get; set; }
        public int AccessId { get; set; }

        public Access Access { get; set; }
    }
}