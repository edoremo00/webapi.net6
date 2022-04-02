using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using testidentityandjwt.BL.Enums;
using testidentityandjwt.BL.Services;
using testidentityandjwt.BL.Utils;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISendEmailService _sendEmailService;

        public EmailController(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        [HttpPost,Route("Sendemail")]
        public async Task<IActionResult> Sendemail(string email, Emailsubjects subject)
        {
            var sendemailresponse= await _sendEmailService.SendEmail(email, subject);
            
            if(sendemailresponse is string)//questo perchè metodo email mi risponde con bool o oggetto Error
            {
                return Ok();
            }
            else
            {
               
                return BadRequest(new Error {StatusCode=400,Message="error in sending email"});
            }
            

        }
    }
}
