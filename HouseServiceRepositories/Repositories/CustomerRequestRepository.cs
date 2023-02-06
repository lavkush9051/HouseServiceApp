using HouseServiceRepositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Repositories
{
    public class CustomerRequestRepository:ICustomerRequestRepository
    {
        HomeServiceDbContext db;

        CustomerRequestRepository(HomeServiceDbContext context)
        {
            this.db = context;
        }
        CustomerRequestRepository()
        {

        }

        public List<CustomersRequest> GetAllCutomersRequest(short id)
        {
            var req = db.CustomersRequests.Where(x => x.OwnerId == id).ToList();
            return req;
        }
    }
}
