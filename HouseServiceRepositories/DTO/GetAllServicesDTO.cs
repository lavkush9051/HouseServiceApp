using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.DTO
{
    public class GetAllServicesDTO
    {
        public short AppointId { get; set; }
        //public short? CustomerId { get; set; }
        public string? OwnerName { get; set; }
        //public short ? YearOfExp { get; set; }
        public string? Service { get; set; }
        public DateTime? DateofAppointment { get; set; }
    }
}
