using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Data
{
    public partial class  Admin
    {
        public short AdminId { get; set; } 
        public string AdminName { get; set; }
        public string? AdminEmail { get; set; }
        public string? AdminPassword { get; set; }
    }
}
