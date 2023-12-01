using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.Infrastructure.Data
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
