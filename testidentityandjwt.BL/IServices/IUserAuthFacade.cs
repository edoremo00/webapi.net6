namespace testidentityandjwt.BL.IServices;

public interface IUserAuthFacade
{
    Task<bool> RegisterUser(Registerdto registeredDto);
}