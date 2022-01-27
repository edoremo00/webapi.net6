using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Userepo _userrepo;

        public AuthController(Userepo userrepo)
        {
            _userrepo = userrepo;
        }

        [HttpPost,Route("Register")]

        public async Task<ActionResult> Register(Registerdto register)
        {
            if(await _userrepo.Registeruser(register))
            {
                return Ok(register);
            }
            return BadRequest();
        }
    }
}
