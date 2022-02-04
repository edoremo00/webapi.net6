using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.Repository
{
   /* public class Userepo:EFRepository<MyUser>
    {
        private readonly jwtandidentitycontext _context;
        //private readonly UserManager<MyUser> _userManager;
        
        //Dont inject DbSet<>, you can just get it off of your context object.
        //I'd recommend looking into the 'Unit of Work' pattern which DbContext follows
        //DbContext is meant to act as the central point for all of your repositories 
        //so you dont have to manually inject each repository everytime you need it, 
        //DbContext also is meant to track your transaction, commit, or rollback.
        
        //Also note: if you look at your context you created, you have two DbSet<MyUser>, 
        //you could probably nuke context.Utenti and use context.Users that is inherited
        //of the class you are inheriting your context class from.
        public Userepo(jwtandidentitycontext context):base(context)
        {
            _context = context;
        }

        public Task<List<MyUser>> GetAll()
        {
            return _context.Users.ToListAsync();
        }
    }*/
}
