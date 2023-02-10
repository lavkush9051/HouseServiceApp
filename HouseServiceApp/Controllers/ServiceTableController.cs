using HouseServiceRepositories.Data;
using HouseServiceRepositories.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//-----------------------admin
namespace HouseServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTableController : ControllerBase
    {
        private IServiceRepository _serviceRepository;

        public ServiceTableController(IServiceRepository serviceRepository)
        {
            this._serviceRepository = serviceRepository;
        }

        // GET: api/<ServiceTableController>
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("/getAllService")]
        public IActionResult Get()
        {
            return Ok(_serviceRepository.GetAllService());
        }

        // GET api/<ServiceTableController>/5
        
        [Authorize(Roles = "Administrator")]
        [HttpPut]
        [Route("/updateService")]
        public IActionResult put(short id, ServicesTable Service)
        {
            return Ok(_serviceRepository.UpdateService(id, Service));
        }

        // POST api/<ServiceTableController>
        
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("addNewService")]
        public IActionResult Post(ServicesTable newService)
        {
            _serviceRepository.AddService(newService);
            return Ok();
        }

       

        // DELETE api/<ServiceTableController>/5
        
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Route("DeleteteService")]
        public IActionResult Delete(short id)
        {
            _serviceRepository.DeleteService(id);
            return Ok();
        }
    }
}
