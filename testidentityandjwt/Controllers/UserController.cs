using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.IServices;
using testidentityandjwt.DAL.Services;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserservice _userservice;

        public UserController(IUserservice userservice)
        {
            _userservice = userservice;
        }

        [HttpGet,Route("Getallusers")]
        public async Task<ActionResult> Getallusers()
        {
           var alluser=await _userservice.Getalluser();
            if(!alluser.Any())
                return NoContent();
            return Ok(alluser);
        }

        [HttpGet,Route("Getsingle/{id}")]
        public async Task<ActionResult> Getsingle(string id)
        {
            MyUser? single = await _userservice.Getsingle(id);
            if (single is null)
                return NotFound(single);
            return Ok(single);


        }
    }
}
