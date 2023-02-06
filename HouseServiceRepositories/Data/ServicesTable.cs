using System;
using System.Collections.Generic;

namespace HouseServiceRepositories.Data
{
    public partial class ServicesTable
    {
        public ServicesTable()
        {
            Owners = new HashSet<Owner>();
        }

        public short ServiceId { get; set; }
        public string? ServiceName { get; set; }

        public virtual ICollection<Owner> Owners { get; set; }
    }
}
