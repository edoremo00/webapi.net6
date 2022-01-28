using Microsoft.AspNetCore.Identity;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.Repository;

public class UserAuthService : IUserAuthService
{
    private readonly UserManager<MyUser> _userManager;

    public UserAuthService(UserManager<MyUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<bool> RegisterUser(Registerdto register)
    {
        MyUser useralreadyregistered = await _userManager.FindByNameAsync(register.Username.ToLower());
        if(useralreadyregistered == null)
        {
            MyUser user = new MyUser()
            {
                Email = register.Email,
                NormalizedEmail=register.Email.ToUpper(),
                UserName = register.Username,
                NormalizedUserName=register.Username.ToUpper(),
                    
                   

            };
            IdentityResult registerresult= await _userManager.CreateAsync(user,register.Password);
            if (!registerresult.Succeeded)
                return false;

            return true;

        }
        return false;
    }
}