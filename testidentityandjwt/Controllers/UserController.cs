using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IServices;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUserservice _userservice;

        private readonly Func<string, IUserservice> _factory;//dal momento che interfaccia IUservice implementata da più classi necessito modo di risolvere dipendenza a runtime in base a un parametro
        //questo metodo contenuto nel delegato mi permette di ottenere la giusta dipendenza a runtime
        //nota che infatti a differenza di altri controller non ho iniettato l'interfaccia direttamente ma il delegato

        public UserController(Func<string, IUserservice> factory)
        {
            _factory= factory;
        }

        [HttpGet,Route("Getallusers")]
        public async Task<ActionResult> Getallusers()
        {
           IUserservice rightservice = _factory("");
            var alluser = await rightservice.Getall();
            if(!alluser.Any())
                return NoContent();
            return Ok(alluser);
        }

        [HttpGet,Route("Getsingle/{id}")]
        public async Task<ActionResult> Getsingle(string id)
        {
            IUserservice rightservice = _factory("");
            UserDTO? single = await rightservice.Getsingle(id);
            if (single is null)
                return NoContent();
            return Ok(single);


        }

        [HttpDelete,Route("deleteuser/{id}")]
        public async Task<ActionResult> deleteuser(string id)
        {
            IUserservice rightservice = _factory("");
            if (await rightservice.Delete(id))
                return Ok(id);
            return NotFound(id);

        }

        [HttpPut,Route("updateuser")]
        public async Task<ActionResult> Update(UserDTO toupdate)
        {
            IUserservice rightservice = _factory("");
            if (await rightservice.Update(toupdate) is not null)
            {
                return Ok(toupdate);
            }
            return NotFound();
        }
    }
}
