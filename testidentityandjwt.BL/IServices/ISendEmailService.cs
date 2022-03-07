using testidentityandjwt.BL.Enums;
using testidentityandjwt.BL.Utils;

namespace testidentityandjwt.BL.Services
{
    public interface ISendEmailService
    {
        Task<object> SendEmail(string email, Emailsubjects emailsubjects);
        Task<bool> SendEmail(string email, Emailsubjects emailsubjects,string body);
    }
}