using HouseServiceRepositories;
using HouseServiceRepositories.Data;
using HouseServiceRepositories.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        IOwnerRepository _ownerRepository;

        public OwnersController(IOwnerRepository ownersRepository)
        {
            this._ownerRepository = ownersRepository;

        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var ownerList = _ownerRepository.Getall() ;
            return Ok(ownerList);
        }

        [HttpGet]
        [Route("GetAllRequests/{id}")]
        public IActionResult GetAllRequests(short id)
        {
            var custReqList = _ownerRepository.GetAllCutomersRequest(id);
            return Ok(custReqList);
        }

        [HttpPost]
        public IActionResult Post(Owner newOwner)
        {
            _ownerRepository.AddNewOwner(newOwner);
            return Ok(newOwner);
        }

        [HttpPut]
        public IActionResult Put(short ownerId, Owner newDetails)
        {
            _ownerRepository.UpdateOwner(ownerId, newDetails);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(short ownerId)
        {
            _ownerRepository.DeleteOwner(ownerId);
            return Ok();
        }
    }
}
