using HouseServiceRepositories;
using HouseServiceRepositories.Data;
using HouseServiceRepositories.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HouseServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminRepository _adminRepository;
        private ICustomerRepository _customerRepository;
        private IOwnerRepository _ownerRepository;
        
        //token
        private IConfiguration _configuration;
        private readonly HomeServiceDbContext _context;

        public AdminController(IAdminRepository adminRepository, ICustomerRepository customerRepository, IOwnerRepository ownerRepository, IConfiguration configuration, HomeServiceDbContext context)
        {
            this._adminRepository = adminRepository;
            this._customerRepository = customerRepository;
            this._ownerRepository = ownerRepository;

            this._configuration = configuration;
            this._context = context;
        }

// Admin 
        [HttpGet]
        [Route("/getAdmin")]
        public IActionResult Get(short id)
        {
            var res = _adminRepository.GetAdmin(id);
            return Ok(res);
        }
//***service list
        [HttpPost]
        [Route("/addNewService")]
        public IActionResult addNewService(ServicesTable newService)
        {
            _adminRepository.AddNewService(newService);
            return Ok(newService);
        }

//****** Customers
        // Get all Customer
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("getAllCustomers")]
        public IActionResult getAllCustomers()
        {
            var allCustomer = _customerRepository.GetCustomers();
            return Ok(allCustomer);
        }
        //Delete customer
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Route("deleteCustomer")]
        public IActionResult deleteCustomer(short id)
        {
            _customerRepository.DeleteCustomer(id);
            return Ok();
        }

// ***** Owners
        // Get all customers 
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("getAllOwner")]
        public IActionResult getAllOwner()
        {
            var AllOwners = _ownerRepository.Getall();
            return Ok(AllOwners);
        }
        // Delete owner
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Route("deleteOwner")]
        public IActionResult deleteOwner(short id)
        {
            _ownerRepository.DeleteOwner(id);
            return Ok();
        }
// ****** Service
        // Delete service
       /* public IActionResult deleteService(short id)
        {
            return Ok();
        }*/
        // Token generate
        [AllowAnonymous]
        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Post(Admin _userData)
        {
            if (_userData != null && _userData.AdminEmail != null && _userData.AdminPassword != null)
            {
                var user = await GetAdmin(_userData.AdminEmail, _userData.AdminPassword);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("AdminId", user.AdminId.ToString()),
                        new Claim("AdminName", user.AdminName),
                        new Claim("AdminEmail", user.AdminEmail),
                        new Claim(ClaimTypes.Role, "Administrator")

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

        private async Task<Admin> GetAdmin(string email, string password)
        {
            return await _context.Admin.FirstOrDefaultAsync(u => u.AdminEmail == email && u.AdminPassword == password);
        }


    }

    public class AdminTokenController : ControllerBase
    {
        //public short AuthenticatedId { get; set; }
        private IConfiguration _configuration;
        private readonly HomeServiceDbContext _context;

        public AdminTokenController(IConfiguration configuration, HomeServiceDbContext context)
        {
            this._configuration = configuration;
            this._context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Admin _userData)
        {
            if (_userData != null && _userData.AdminName != null && _userData.AdminPassword != null)
            {
                var user = await GetUser(_userData.AdminName, _userData.AdminPassword);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("AdminId", user.AdminId.ToString()),
                        new Claim("AdminName", user.AdminName)

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

        private async Task<Admin> GetUser(string name, string password)
        {
            return await _context.Admin.FirstOrDefaultAsync(u => u.AdminPassword == name && u.AdminPassword == password);
        }
    }
}
