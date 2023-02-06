using HouseServiceRepositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Repositories
{
    public interface ICustomerRequestRepository
    {
        List<CustomersRequest> GetAllCutomersRequest(short id);
    }
}
