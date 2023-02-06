using System;
using System.Collections.Generic;

namespace HouseServiceRepositories.Data
{
    public partial class Customer
    {
        public Customer()
        {
            CustomersRequests = new HashSet<CustomersRequest>();
            ServicesLists = new HashSet<ServicesList>();
        }

        public short CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public long? Contact { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; } 

        public virtual ICollection<CustomersRequest> CustomersRequests { get; set; }
        public virtual ICollection<ServicesList> ServicesLists { get; set; }
    }
}
