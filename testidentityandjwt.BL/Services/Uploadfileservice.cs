using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.BL.Services
{
    public class Uploadfileservice: EFRepository, IUploadfile
    {
        private readonly IConfiguration _configuration;
        

        public Uploadfileservice( IConfiguration configuration,jwtandidentitycontext context):base(context)
        {
            _configuration = configuration;

        }

        
       public async Task<string> UploadFile(IFormFile file) 
        {
            
           
            BlobContainerClient blobContainerClient = new BlobContainerClient(_configuration.GetRequiredSection("Blob:Connectionstring").Value,_configuration.GetSection("Blob:containername").Value);
            
            List<string> supportedfilesformats = new List<string> { ".jpg", ".png", ".jpeg" };
            if(file is not null && file.Length > 0)
            {
                if (!supportedfilesformats.Contains(Path.GetExtension(file.FileName)))
                {
                    return "formato non supportato";
                }
              string newfileneme=String.Concat(Guid.NewGuid(),Path.GetExtension(file.FileName));
              BlobClient blobClient = blobContainerClient.GetBlobClient(newfileneme);//senza questo passaggio l'url lo da di tutto il container non del file specifico
              var uploadresponse=await blobContainerClient.UploadBlobAsync(String.Concat(Guid.NewGuid(),Path.GetExtension(file.FileName)), file.OpenReadStream());
                if (uploadresponse.GetRawResponse().Status == 201)
                    return blobClient.Uri.AbsoluteUri;

            }
            return "file not selected";
        }

        public async Task<string> UploadFile(IFormFile file,string foruserid)
        {
            if (string.IsNullOrWhiteSpace(foruserid)) return "foruserid field is required";
            MyUser? usertouploadfile = jwtandidentitycontext.Users.Find(foruserid);
            if (usertouploadfile is null) return "User not Found";
            
            BlobContainerClient blobContainerClient = new BlobContainerClient(_configuration.GetRequiredSection("Blob:Connectionstring").Value, _configuration.GetSection("Blob:containername").Value);

            List<string> supportedfilesformats = new List<string> { ".jpg", ".png", ".jpeg" };
            if (file is not null && file.Length > 0)
            {
                if (!supportedfilesformats.Contains(Path.GetExtension(file.FileName)))
                {
                    return "formato non supportato";
                }
                string newfileneme = String.Concat(Guid.NewGuid(), Path.GetExtension(file.FileName));
                BlobClient blobClient = blobContainerClient.GetBlobClient(newfileneme);//senza questo passaggio l'url lo da di tutto il container non del file specifico
                var uploadresponse = await blobContainerClient.UploadBlobAsync(String.Concat(Guid.NewGuid(), Path.GetExtension(file.FileName)), file.OpenReadStream());
                if (uploadresponse.GetRawResponse().Status == 201)
                    return blobClient.Uri.AbsoluteUri;

            }
            return "file not selected";
        }



        public async Task<bool> Deletefile(string filename)
        {

            BlobContainerClient blobContainerClient = new BlobContainerClient(_configuration.GetRequiredSection("Blob:Connectionstring").Value, _configuration.GetSection("Blob:containername").Value);
            if (!string.IsNullOrWhiteSpace(filename))
            {
               return await blobContainerClient.DeleteBlobIfExistsAsync(filename);
            }
            return false;
        }

        public async Task<object> Getallfilesincontainer()
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(_configuration.GetRequiredSection("Blob:Connectionstring").Value, _configuration.GetSection("Blob:containername").Value);
            var  allblobs =blobContainerClient.GetBlobs()
                .Select
                (x=>new 
                    {Name=x.Name,deleted=x.Deleted,createdon=x.Properties.CreatedOn,lastmodified=x.Properties.LastModified}
                ).Take(10);
            /* var prova=from blob in allblobs
                       select new {Name=blob.Name,deleted=blob.Deleted,};//così funziona perccè con lambda no?
             return prova;*/
            return allblobs;
            
        }

     
    }
}
