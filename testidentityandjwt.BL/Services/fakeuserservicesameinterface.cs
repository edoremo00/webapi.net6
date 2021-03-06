using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IMapper;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.BL.Services
{
    public class fakeuserservicesameinterface:EFRepository,IUserservice
    {
        private readonly IDatamapper _mapper;

        public fakeuserservicesameinterface(jwtandidentitycontext context, IDatamapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();

        }



        public Task<UserDTO?> Update(UserDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDTO>> Getall()
        {
            return await jwtandidentitycontext.Users.Select(u => _mapper.mapmyusertodto(u)).AsNoTracking().ToListAsync();
        }

        Task<UserDTO?> IUserservice.Getsingle(string id)
        {
            throw new NotImplementedException();
        }
    }


}
