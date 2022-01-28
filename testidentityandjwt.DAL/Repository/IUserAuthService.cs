using testidentityandjwt.DAL.DTO;

namespace testidentityandjwt.DAL.Repository;

public interface IUserAuthService
{
    Task<bool> RegisterUser(Registerdto register);
}