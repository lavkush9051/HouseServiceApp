using HouseServiceRepositories.Data;
using HouseServiceRepositories.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories
{
    public interface ICustomerRepository
    {
            List<Customer> GetCustomers();
            Customer GetCustomer(short id);
            void PostCustomer(Customer newcust);
            void DeleteCustomer(short id);
            void UpdateCustomerDetails(short id, Customer newDetails);
            List<GetAllServicesDTO> GetallServices(short id);



    }
}
