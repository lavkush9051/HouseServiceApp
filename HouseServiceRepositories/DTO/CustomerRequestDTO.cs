using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.DTO
{
    public class CustomerRequestDTO
    {
        public short AppointmentId { get; set; }
        //public short? CustomerId { get; set; }
        public string? CustomersName { get; set; } 
        public string? CustomersAddress { get; set; }    
        public DateTime? DateOfAppointment { get; set; }
        //public short OwnerId { get; set; }
    }
}
