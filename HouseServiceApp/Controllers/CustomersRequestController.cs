/*using HouseServiceRepositories;
using HouseServiceRepositories.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersRequestController : ControllerBase
    {
        private ICustomerRequestRepository _customerReqRepo;

        public CustomersRequestController(ICustomerRequestRepository customerReqRepo)
        {
            this._customerReqRepo = customerReqRepo;

        }

        [HttpGet]
        public IActionResult Get(short id)
        {
            var custReqList = _customerReqRepo.GetAllCutomersRequest(id);
            return Ok(custReqList);
        }
    }
}
*/