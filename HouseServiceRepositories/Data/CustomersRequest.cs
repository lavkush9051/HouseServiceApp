using System;
using System.Collections.Generic;

namespace HouseServiceRepositories.Data
{
    public partial class CustomersRequest
    {
        public short AppointmentId { get; set; }
        public short? CustomerId { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public short OwnerId { get; set; }  
        public virtual Customer? Customer { get; set; }
        public virtual Owner? Owner { get; set; }
    }
}
