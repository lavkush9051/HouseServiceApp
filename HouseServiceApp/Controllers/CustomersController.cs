using Microsoft.AspNetCore.Http;
using HouseServiceRepositories;
using Microsoft.AspNetCore.Mvc;
using HouseServiceRepositories.Data;
using Microsoft.AspNetCore.Authorization;
using static HouseServiceApp.Controllers.TokenController;

namespace HouseServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        //HomeServiceDbContext db;
        /*public CustomersController(HomeServiceDbContext context)
        {
            db = context;
        }*/

        private ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;

        }
        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("getAllCustomers")]
        public IActionResult Get()
        {
            
            var custList = _customerRepository.GetCustomers();
            return Ok(custList);
        }

/*        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(short id)
        {
            var cust = _customerRepository.GetCustomer(id);
            return Ok(cust);
        }*/

        
        [HttpPost]
        [Route("addNewCustomer")]
        public IActionResult Post(Customer Newcust)
        {
           
            _customerRepository.PostCustomer(Newcust);
            /*db.Customers.Add(Newcust);
            db.SaveChanges();*/
            return Ok(Newcust);
            
        }

        // customer request
        
        //AuthenticatedId
        
        [Authorize]
        [HttpGet]
        [Route("{id}getCustomerServices")]
        public IActionResult Get(short id)
        {

            var task = _customerRepository.GetallServices(id);
            Console.WriteLine();    
            return Ok(task);
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteCustomer")]
        public IActionResult Delete(short id)
        {
            _customerRepository.DeleteCustomer(id);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("updateCustomer")]
        public IActionResult Put(short id, Customer newDetails)
        {
            _customerRepository.UpdateCustomerDetails(id, newDetails);
            return Ok();
        }
    }
}
