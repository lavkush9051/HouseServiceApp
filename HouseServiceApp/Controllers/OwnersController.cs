using Microsoft.AspNetCore.Http;

using HouseServiceRepositories;
using HouseServiceRepositories.Data;
using HouseServiceRepositories.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    public class OwnersController : ControllerBase
    {
        private IOwnerRepository _ownerRepository;
        private IConfiguration _configuration;
        private readonly HomeServiceDbContext _context;

        public OwnersController(IOwnerRepository ownersRepository, IConfiguration configuration, HomeServiceDbContext context)
        {
            this._ownerRepository = ownersRepository;
            this._configuration = configuration;
            this._context = context;

        }
        // Get all owner
        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("GetAllOwner")]
        public IActionResult GetAll()
        {
            var ownerList = _ownerRepository.Getall();
            return Ok(ownerList);
        }
        // Get All request of a Owner 
        //[Authorize(Roles = "Owner")]
        [HttpGet]
        [Route("GetAllRequests")]
        public IActionResult GetAllRequests(short id)
        {
            var custReqList = _ownerRepository.GetAllCutomersRequest(id);
            return Ok(custReqList);
        }
        // Add New Owner
        //[Authorize(Roles = "Owner,Administrator")]
        [HttpPost]
        [Route("/AddNewOwner")]
        public IActionResult Post(Owner newOwner)
        {
            _ownerRepository.AddNewOwner(newOwner);
            return Ok(newOwner);
        }
        // Update Owner
        //[Authorize(Roles = "Owner")]
        [HttpPut]
        [Route("/UpdateOwner")]
        public IActionResult Put(short ownerId, Owner newDetails)
        {
            _ownerRepository.UpdateOwner(ownerId, newDetails);
            return Ok();
        }
        // Delete Owner

        //[Authorize(Roles = "Owner, Administrator")]
        [HttpDelete]
        [Route("/DeleteOwner")]
        public IActionResult Delete(short ownerId)
        {
            _ownerRepository.DeleteOwner(ownerId);
            return Ok();
        }

        // Login owner
        [AllowAnonymous]
        [HttpPost]
        //[Route("/Login")]
        //[Route("/CustomerLogin")]
        public async Task<IActionResult> Login(Owner _ownerData)
        {
            if (_ownerData != null && _ownerData.OwnerEmailId != null && _ownerData.OwnerPassword != null)
            {
                var user = await GetOwner(_ownerData.OwnerEmailId, _ownerData.OwnerPassword);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("OwnerId", user.OwnerId.ToString()),
                        new Claim("OwnerName", user.OwnerName),
                        new Claim("OwnerEmail", user.OwnerEmailId),
                        new Claim(ClaimTypes.Role, "Owner")

                    };
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

        private async Task<Owner> GetOwner(string email, string password)
        {
            return await _context.Owners.FirstOrDefaultAsync(u => u.OwnerEmailId == email && u.OwnerPassword == password);
        }
    }
}
