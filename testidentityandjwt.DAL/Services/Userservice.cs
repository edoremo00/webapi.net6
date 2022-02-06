using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.IServices;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.DAL.Services
{
    public class Userservice : EFRepository,IUserservice
    {
        //private readonly jwtandidentitycontext _context; NON MI SERVE CHIAMA COSTRUTTORE BASE
        public Userservice(jwtandidentitycontext context) : base(context)
        {
           // _context = context;
        }

       
        public async Task<bool> Delete(string id)
        {
           MyUser? todelete= await Getsingle(id);
            if(todelete is not null)
            {
                jwtandidentitycontext.Remove(todelete);
                
            }
            return await jwtandidentitycontext.SaveChangesAsync() > 0;
              
        }

        public virtual async Task<List<MyUser>> Getall()
        {
            var allusers = await jwtandidentitycontext.Users.ToListAsync();
            if(!allusers.Any())
                return new List<MyUser>();
            return allusers;
        }

        public async Task<MyUser?> Getsingle(string id)
        {
            MyUser? user=await jwtandidentitycontext.Users.FindAsync(id);
            if(user is null)
                return null;
            return user;
        }

        public MyUser Update(MyUser entity)
        {
           throw new NotImplementedException();
        }
    }
}
