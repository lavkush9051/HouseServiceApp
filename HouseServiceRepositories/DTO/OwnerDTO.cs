using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.DTO
{
    public class OwnerDTO
    {
        public short OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? Service { get; set; }
        public short? YearOfExp { get; set; }
        public short? CostToService { get; set; }

    }
}
