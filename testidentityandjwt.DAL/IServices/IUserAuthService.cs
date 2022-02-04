using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using testidentityandjwt.DAL.DTO;

namespace testidentityandjwt.DAL.Repository;

public interface IUserAuthService
{
    Task<bool> RegisterUser(Registerdto register);
    JwtSecurityToken createtoken(List<Claim> userclaim);
    Task<JwtSecurityToken?> Login(LoginDTO loginDTO);
}