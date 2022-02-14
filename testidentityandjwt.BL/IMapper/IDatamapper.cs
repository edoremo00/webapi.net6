using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.BL.IMapper
{
    public interface IDatamapper//andrebbe resa generica
    {
        UserDTO mapmyusertodto(MyUser tomap);
    }
}
