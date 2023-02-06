using HouseServiceRepositories.Data;
using HouseServiceRepositories.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceRepository.GetAllService());
        }

        // GET api/<ServiceTableController>/5
        [HttpPut("{id}")]
        public IActionResult put(short id, ServicesTable Service)
        {
            return Ok(_serviceRepository.UpdateService(id, Service));
        }

        // POST api/<ServiceTableController>
        [HttpPost]
        public IActionResult Post(ServicesTable newService)
        {
            _serviceRepository.AddService(newService);
            return Ok();
        }

        //[HttpPut("{id}")]
        /*public IActionResult Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE api/<ServiceTableController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(short id)
        {
            _serviceRepository.DeleteService(id);
            return Ok();
        }
    }
}
