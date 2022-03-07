using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testidentityandjwt.BL.Services;

namespace testidentityandjwt.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class ServicebusController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public ServicebusController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost,Route("sendmessagetoqueue")]
        public async Task<IActionResult> sendmessagetoqueue()
        {
            _queueService.SendMessagetoqueue();
            return Ok();
        }

       /* [HttpGet,Route("processqueuemessages")]
        public async Task<IActionResult> processqueuemessages()
        {
            
            return Ok(await _serivcebusservice.Getmessagesfromqueue());
        }*/
  //  }
}
