using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.Mapper
{
    public class Datamapper : IDatamapper
    {
        public UserDTO mapmyusertodto(MyUser tomap)
        {
            return new UserDTO
            {
                Email = tomap.Email,
                Userid = tomap.Id,
                Birthday = tomap.birthday,
                Username=tomap.UserName

            };
        }
    }
}
