using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testidentityandjwt.DAL.IServices;
using testidentityandjwt.DAL.Services;

namespace testidentityandjwt.Controllers
{
   /* [Route("api/[controller]")]
    [ApiController]
    public class FakeuserController : ControllerBase
    {
        private readonly IUserservice _fakeuserservice;

        public FakeuserController(IUserservice fakeuserservice)
        {
            _fakeuserservice = fakeuserservice; 
        }

        [HttpGet,Route("fakeusergetall")]
        public async Task< ActionResult> fakeusergetall()
        {
            return Ok(await _fakeuserservice.Getall());
        }
    }*/
}
