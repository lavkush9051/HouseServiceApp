/*using HouseServiceRepositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HouseServiceApp.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        //public short AuthenticatedId { get; set; }
        private IConfiguration _configuration;
        private readonly HomeServiceDbContext _context;

        public TokenController(IConfiguration configuration, HomeServiceDbContext context)
        {
            this._configuration = configuration;
            this._context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Customer _userData)
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
                        new Claim("Email", user.EmailId)

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
*/