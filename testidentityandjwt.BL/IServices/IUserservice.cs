using testidentityandjwt.BL.DTO;

namespace testidentityandjwt.BL.IServices
{
    public interface IUserservice
    {
        Task<List<UserDTO>> Getall();
        Task<UserDTO?> Getsingle(string id);

        Task<UserDTO?> Update(UserDTO entity);
        Task<bool> Delete(string id);

    }
}
