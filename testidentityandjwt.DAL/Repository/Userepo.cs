using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.Repository
{
    public class Userepo:EFRepository<MyUser>
    {
        private readonly UserManager<MyUser> _userManager;
        public Userepo(jwtandidentitycontext context,DbSet<MyUser> usertable,UserManager<MyUser>userManager):base(context,usertable) { 
        
            _userManager = userManager;
        }

        public async Task<bool> Registeruser(Registerdto register)
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
}
