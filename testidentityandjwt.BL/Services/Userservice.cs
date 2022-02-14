﻿using Microsoft.EntityFrameworkCore;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IMapper;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.BL.Services
{
    public class Userservice : EFRepository, IUserservice
    {
        //private readonly jwtandidentitycontext _context; NON MI SERVE CHIAMA COSTRUTTORE BASE
        private readonly IDatamapper _mapper;

        public Userservice(jwtandidentitycontext context, IDatamapper mapper) : base(context)
        {
            // _context = context;
            _mapper = mapper;
        }


        public async Task<bool> Delete(string id)
        {
            MyUser? todelete = await Getsinglemyuser(id);
            if (todelete is not null && todelete.IsDeleted == false)
            {
                jwtandidentitycontext.Update(todelete);
                todelete.IsDeleted = true;

            }
            return await jwtandidentitycontext.SaveChangesAsync() > 0;

        }

        public virtual async Task<List<UserDTO>> Getall()
        {
            var allusers = await jwtandidentitycontext.Users.Where(u => u.IsDeleted == false).AsNoTracking().ToListAsync();
            if (!allusers.Any())
                return new List<UserDTO>();
            return allusers.Select(u => _mapper.mapmyusertodto(u)).ToList();
        }

        public async Task<UserDTO?> Getsingle(string id)
        {
            MyUser? user = await jwtandidentitycontext.Users.FindAsync(id);
            if (user is null || user.IsDeleted)
                return null;
            return _mapper.mapmyusertodto(user);
        }

        private async Task<MyUser?> Getsinglemyuser(string id) //get single che opera direttamente su db senza DTO
        {
            MyUser? tosearch = await jwtandidentitycontext.Users.FindAsync(id);
            if (tosearch is not null)
                return tosearch;
            return null;
        }

        public async Task<object?> GenericGetsingle(string id, bool mappeduser = false)
        {
            MyUser? user = await jwtandidentitycontext.Users.FindAsync(id);
            if (user is null || user.IsDeleted)
                return null;
            if (mappeduser)
                return _mapper.mapmyusertodto(user);
            return user;


        }

        public async Task<UserDTO?> Update(UserDTO entity)
        {
            //MyUser? usertoupdate =   await Getsinglemyuser(entity.Userid);
            MyUser? usertoupdate = (MyUser?)await GenericGetsingle(entity.Userid);
            if (usertoupdate is not null)
            {
                usertoupdate.UserName = entity.Username;
                usertoupdate.NormalizedUserName = entity.Username.ToUpper();
                usertoupdate.birthday = entity.Birthday;
                jwtandidentitycontext.Update(usertoupdate);
                if (await jwtandidentitycontext.SaveChangesAsync() > 0)
                    return _mapper.mapmyusertodto(usertoupdate);

            }
            return null;

        }


    }
}

