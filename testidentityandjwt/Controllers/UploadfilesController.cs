using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.BL.Utils;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadfilesController : ControllerBase
    {
        private readonly IUploadfile _uploadifile;

        public UploadfilesController(IUploadfile uploadfile)
        {
            _uploadifile = uploadfile;
        }

        [HttpPost,Route("Uploadfile")]
        public async Task<IActionResult> Uploadfile(IFormFile file)
        {
            string response = await _uploadifile.UploadFile(file);
            if (response== "formato non supportato")
            {
                return StatusCode(StatusCodes.Status415UnsupportedMediaType,new Error { StatusCode=415,Message="unsupported file extension"});
                
            }else if (response=="file not selected")
            {
                return BadRequest(new Error { StatusCode=400,Message="no file selected"});
            }
            return Ok(JsonSerializer.Serialize(response));

        }

        [HttpDelete,Route("Deletefile")]
        public async Task<IActionResult> Deletefile(string filename)
        {
            bool response = await _uploadifile.Deletefile(filename);
            if (response)
            {
                return Ok(JsonSerializer.Serialize(response));
            }
            return BadRequest(new Error { StatusCode=400,Message= $"file {filename} not found" });

           
        }

        [HttpGet,Route("Getallblobsintodocontainer")]
        public async Task<IActionResult> Getallblobsintodocontainer()
        {
            return Ok(await _uploadifile.Getallfilesincontainer());
        }
    }
}
