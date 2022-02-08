using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.Mapper
{
    public interface IDatamapper
    {
        UserDTO mapmyusertodto(MyUser tomap);
    }
}