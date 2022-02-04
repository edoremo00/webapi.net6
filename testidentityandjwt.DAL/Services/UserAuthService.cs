using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.Repository;

public class UserAuthService : IUserAuthService
{
    private readonly UserManager<MyUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserAuthService(UserManager<MyUser> userManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public JwtSecurityToken createtoken(List<Claim> userclaim)
    {
        var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: userclaim,
            signingCredentials: new SigningCredentials(signingkey,SecurityAlgorithms.HmacSha256)

            );
        return token;
       
    }

    public async Task<bool> RegisterUser(Registerdto register)
    {
        MyUser useralreadyregistered = await _userManager.FindByNameAsync(register.Username.ToLower());
        if(useralreadyregistered is null)
        {
            MyUser user = new MyUser()
            {
                Email = register.Email,
                NormalizedEmail=register.Email.ToUpper(),
                UserName = register.Username,
                NormalizedUserName=register.Username.ToUpper(),
                birthday=register.birthday.Date,
                    
                   

            };
            IdentityResult registerresult= await _userManager.CreateAsync(user,register.Password);
            if (!registerresult.Succeeded)
                return false;

            return true;

        }
        return false;
    }

    public async Task<JwtSecurityToken?> Login(LoginDTO login)
    {
        MyUser checkifexist=await _userManager.FindByEmailAsync(login.email.ToLower());
        if(checkifexist is null)
            return null;
        bool checkpass=await _userManager.CheckPasswordAsync(checkifexist,login.Password);
        if (checkpass)
        {
            List<Claim> userclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,checkifexist.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            JwtSecurityToken token=createtoken(userclaims);

            return token;
        }
        return null;
            

    }
}