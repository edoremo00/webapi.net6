using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testidentityandjwt.DAL.DTO;
using testidentityandjwt.DAL.IServices;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeuserservicesameController : ControllerBase
    {
        
        private readonly Func<string, IUserservice> _factory;//dal momento che interfaccia IUservice implementata da più classi necessito modo di risolvere dipendenza a runtime in base a un parametro
        //questo metodo contenuto nel delegato mi permette di ottenere la giusta dipendenza a runtime
        //nota che infatti a differenza di altri controller non ho iniettato l'interfaccia direttamente ma il delegato

        public FakeuserservicesameController(Func<string,IUserservice> factory)
        {

           _factory= factory;   
        }

        [HttpGet,Route("Getallfakeusersameinterfacecontroller")]
        public async Task<IActionResult> Getallfakeusersameinterfacecontroller()
        {
            IUserservice userserviceinterface = _factory("fakeuserservicesameinterface");//chiama il program cs e mi ritorna un IUservice(interfaccia) del giusto servizio
            var allusers =await userserviceinterface.Getall();//uso istanza ottenuta sopra per eseguire il metodo
           
            return Ok(allusers);
        }

        [HttpGet, Route("Getsingle/{id}")]
        public async Task<ActionResult> Getsingle(string id)
        {
            IUserservice userservicesameinterface = _factory("fakeuserservicesameinterface");
            UserDTO? single = await userservicesameinterface.Getsingle(id);
            if (single is null)
                return NotFound(single);
            return Ok(single);


        }

        [HttpDelete, Route("deleteuser/{id}")]
        public async Task<ActionResult> deleteuser(string id)
        {
            IUserservice userservicesameinterface = _factory("fakeuserservicesameinterface");
            if (await userservicesameinterface.Delete(id))
                return Ok(id);
            return NotFound(id);

        }

        [HttpPut, Route("updateuser")]
        public async Task<ActionResult> Update(UserDTO toupdate)
        {
            IUserservice userservicesameinterface = _factory("fakeuserservicesameinterface");
            if (await userservicesameinterface.Update(toupdate) is not null)
            {
                return Ok(toupdate);
            }
            return BadRequest();
        }
    }
}
