using HouseServiceRepositories.Data;
using HouseServiceRepositories.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories
{
    public class CustomerRepository:ICustomerRepository
    {
        HomeServiceDbContext db;

        public CustomerRepository(HomeServiceDbContext context)
        {
            this.db = context;
        }
        public CustomerRepository()
        {
 
        }

        public List<Customer> GetCustomers()
        {
            return db.Customers.ToList();
        }
        public Customer GetCustomer(short id)
        {
            var cust = db.Customers.Find(id);
            return cust== null ? null : cust;
        }

        public void PostCustomer(Customer newCust) {
            db.Customers.Add(newCust);
            db.SaveChanges();
        }

        public List<GetAllServicesDTO> GetallServices(short id)
        {
            var serviceList = db.ServicesLists.Where(x => x.CustomerId == id).Select(x => new GetAllServicesDTO
            {
                AppointId= x.AppointId,
                OwnerName = x.Owner.OwnerName,
                DateofAppointment= x.DateofAppointment,
                Service = x.Owner.Service.ServiceName
            }).ToList();
            return serviceList;
        }

        public void DeleteCustomer(short id)
        {
            //db.Customers.Remove(id);
            try
            {
                Customer cust = db.Customers.Find(id);       
                if(cust != null)
                {
                    db.Customers.Remove(cust);
                    db.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCustomerDetails(short id, Customer newDetails)
        {
            

            try
            {
                Customer oldDetails = db.Customers.Find(id);
                if(oldDetails != null)
                {
                    oldDetails.CustomerName = newDetails.CustomerName;
                    oldDetails.CustomerAddress = newDetails.CustomerAddress;
                    oldDetails.ServicesLists = newDetails.ServicesLists;
                    oldDetails.Contact = newDetails.Contact;
                    oldDetails.EmailId = newDetails.EmailId;
                    oldDetails.Password = newDetails.Password;
                    db.SaveChanges();
                }
                
            }
            catch
            {
                throw;
            }
        }

        
    }
}
