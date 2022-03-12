using testidentityandjwt.BL.Enums;

namespace testidentityandjwt.BL.Services
{
    public interface ISendEmailService
    {
        Task<object> SendEmail(string email, Emailsubjects emailsubjects);
        Task<bool> SendEmail(string email, Emailsubjects emailsubjects,string body);
        object OnRegisteredUser(object source, UserArgs args);
        object OnUserinfochanged(object source, UserArgs userinfoargs);

        //Task<object> SendEmailtoRegisteredUser(string email);
    }
}