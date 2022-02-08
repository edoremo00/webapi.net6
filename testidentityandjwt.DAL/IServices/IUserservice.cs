using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.IServices
{
    public interface IUserservice
    {
        Task<List<UserDTO>> Getall();
        Task<UserDTO?> Getsingle(string id);

        Task<UserDTO?> Update(UserDTO entity);
        Task<bool>Delete(string id);

    }
}
