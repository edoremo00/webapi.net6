using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testidentityandjwt.DAL.IServices;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeuserservicesameController : ControllerBase
    {
        private readonly IUserservice _userservice;
        //private readonly Func<string, IUserservice> _factory;

        public FakeuserservicesameController( IUserservice userservice)
        {
            _userservice = userservice;
           //_factory= factory;   
        }

        [HttpGet,Route("Getallfakeusersameinterfacecontroller")]
        public async Task<IActionResult> Getallfakeusersameinterfacecontroller()
        {
            return Ok(await _userservice.Getall());
        }
    }
}
