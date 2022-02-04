using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
         private readonly IUserAuthService _authService;
       

        public AuthController(
             IUserAuthService authService
       
            )
        {
             _authService = authService;
          
        }

         [HttpPost,Route("Register")]

         public async Task<ActionResult> Register(Registerdto register)
         {
             if(await _authService.RegisterUser(register))
            {
                 return Ok(register);
            }
            return BadRequest();
         }

        [HttpPost,Route("Login")]
        public async Task<ActionResult> Login(LoginDTO login)
        {
          JwtSecurityToken? tok=  await _authService.Login(login);
            if(tok is null)
            {
                return BadRequest();
            }
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(tok),
                expiration = tok.ValidTo
            });
        }

        
    }
}
