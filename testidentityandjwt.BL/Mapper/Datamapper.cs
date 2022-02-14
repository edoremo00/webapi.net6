using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IMapper;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.BL.IServices
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
                Username = tomap.UserName

            };
        }
    }
}
