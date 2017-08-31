using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthStudy.DAL.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<GroupAccess> GroupAccess { get; set; }
    }
}
