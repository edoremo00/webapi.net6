using testidentityandjwt.BL.Enums;
using testidentityandjwt.BL.IServices;

namespace testidentityandjwt.BL.Services
{
    public interface IQueueService
    {
        Task SendMessagetoqueue<T>(T message, string queuename, Eventlabelsservicebus eventlabelname);
    }
}