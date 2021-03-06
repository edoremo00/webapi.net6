using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.BL.Services;
using testidentityandjwt.BL.Utils;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
         private readonly IUserAuthService _authService;
        //private readonly IUserAuthFacade _userAuthFacade;
       

        public AuthController(
             IUserAuthService authService
             //IUserAuthFacade userAuthFacade
       
            )
        {
             _authService = authService;
            //_userAuthFacade = userAuthFacade;
             
          
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
                expiration = tok.ValidTo,
                claims=tok.Claims
            });
        }

        [HttpPost,Route("ValidateGoogletoken")]
        public async Task<IActionResult> ValidateGoogleToken(string token)
        {
           var result= await _authService.ValidateGoogletoken(token);
            if(result is null)
            {
                return BadRequest("Invalid Google token");
            }else if(result is Error)
            {
                return BadRequest(result);
            }
            else//ritorna token JWT
            {
                return Ok(result);
            }
        }

        
    }
}
