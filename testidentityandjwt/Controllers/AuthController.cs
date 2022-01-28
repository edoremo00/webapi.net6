using Microsoft.AspNetCore.Mvc;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // private readonly IUserAuthService _authService;
        private readonly Userepo _userepo;

        public AuthController(
            // IUserAuthService authService, 
            Userepo userepo)
        {
            // _authService = authService;
            _userepo = userepo;
        }

        // [HttpPost,Route("Register")]

        // public async Task<ActionResult> Register(Registerdto register)
        // {
        //     if(await _authService.RegisterUser(register))
        //     {
        //         return Ok(register);
        //     }
        //     return BadRequest();
        // }

        [HttpGet, Route("GetUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userepo.GetAll();
            if (!users.Any())
            {
                return NoContent();
            }

            return Ok(users);
        }
    }
}
