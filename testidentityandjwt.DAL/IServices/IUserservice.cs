using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.IServices
{
    public interface IUserservice
    {
        Task<List<MyUser>> Getalluser();
        Task<MyUser?> Getsingle(string id);

    }
}
