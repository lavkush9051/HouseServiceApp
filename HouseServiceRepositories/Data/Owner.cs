using System;
using System.Collections.Generic;

namespace HouseServiceRepositories.Data
{
    public partial class Owner
    {
        public Owner()
        {
            ServicesLists = new HashSet<ServicesList>();
        }

        public short OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public short? ServiceId { get; set; }
        public short? YearOfExp { get; set; }
        public short? CostToService { get; set; }
        public string? OwnerEmailId { get; set; }   
        public string? OwnerPassword { get; set; }  

        public virtual ServicesTable? Service { get; set; }
        public virtual ICollection<ServicesList> ServicesLists { get; set; }
    }
}
