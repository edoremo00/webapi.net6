using testidentityandjwt.BL.Enums;

namespace testidentityandjwt.BL.Services
{
    public interface ISendEmailService
    {
        Task<object> SendEmail(string email, Emailsubjects emailsubjects);
        Task<bool> SendEmail(string email, Emailsubjects emailsubjects,string body);
        Task<object> OnRegisteredUser(object source, EventArgs args);
    }
}