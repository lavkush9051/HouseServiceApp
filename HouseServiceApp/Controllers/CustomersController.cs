using Microsoft.AspNetCore.Http;
using HouseServiceRepositories;
using Microsoft.AspNetCore.Mvc;
using HouseServiceRepositories.Data;
using Microsoft.AspNetCore.Authorization;
//using static HouseServiceApp.Controllers.TokenController;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HouseServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private ICustomerRepository _customerRepository;
        private IConfiguration _configuration;
        private readonly HomeServiceDbContext _context;

        public CustomersController(ICustomerRepository customerRepository, IConfiguration configuration, HomeServiceDbContext context)
        {
            this._customerRepository = customerRepository;
            this._configuration = configuration;
            this._context = context;

        }

        // get all Customer
        //[Authorize(Roles = "Administrator,Customer")]
        [HttpGet]
        [Route("/getAllCustomers")]
        public IActionResult Get()
        {

            var custList = _customerRepository.GetCustomers();
            return Ok(custList);
        }

        // View a Customer
        [Authorize(Roles = "Customer")]
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetProfile(short id)
        {
            var cust = _customerRepository.GetCustomer(id);
            return Ok(cust);
        }
        // Add New Customer
        [AllowAnonymous]
        [HttpPost]
        [Route("/addNewCustomer")]
        public IActionResult Post(Customer Newcust)
        {

            _customerRepository.PostCustomer(Newcust);
            return Ok(Newcust);

        }
        //book appointmet for a service
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [Route("/book Appointment")]
        public IActionResult BookAppointment(CustomersRequest newAppointment)
        {
            var res  = _customerRepository.AddNewAppointment(newAppointment);
            return Ok(res);
        }

        // Get AllService for particular customer
        //[Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("getCustomerServices/{id}")]
        public IActionResult Get(short id)
        {

            var task = _customerRepository.GetallServices(id);
            Console.WriteLine();
            return Ok(task);
        }
        // Delete Customer
        //[Authorize(Roles = "Customer,Administrator")]
        [HttpDelete]
        [Route("/deleteCustomer")]
        //public IActionResult Delete(short id, string name)
        public IActionResult Delete(short id)
        {
            _customerRepository.DeleteCustomer(id);
            return Ok();
        }
        // Update Customer
        [Authorize(Roles = "Customer")]
        [HttpPut]
        [Route("/updateCustomer")]
        public IActionResult Put(short id, Customer newDetails)
        {
            _customerRepository.UpdateCustomerDetails(id, newDetails);
            return Ok();
        }

        // Login
        //dynamic token;
        [AllowAnonymous]
        [HttpPost]
        [Route("/CustomerLogin")]
        public async Task<IActionResult> Login(Customer _userData)
        {
            if (_userData != null && _userData.EmailId != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.EmailId, _userData.Password);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("CustomerId", user.CustomerId.ToString()),
                        new Claim("CustomerName", user.CustomerName),
                        new Claim("Email", user.EmailId),
                        new Claim(ClaimTypes.Role, "Customer")

                    };
                    //AuthenticatedId = user.CustomerId;
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Customer> GetUser(string email, string password)
        {
            return await _context.Customers.FirstOrDefaultAsync(u => u.EmailId == email && u.Password == password);
        }
    }
}
