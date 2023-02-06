using System;
using System.Collections.Generic;

namespace HouseServiceRepositories.Data
{
    public partial class ServicesList
    {
        public short AppointId { get; set; }
        public short? CustomerId { get; set; }
        public short? OwnerId { get; set; }
        public DateTime? DateofAppointment { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Owner? Owner { get; set; }
    }
}
