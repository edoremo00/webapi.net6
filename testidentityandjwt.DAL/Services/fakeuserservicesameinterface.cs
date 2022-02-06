using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.IServices;

namespace testidentityandjwt.DAL.Services
{
    public class fakeuserservicesameinterface : IUserservice
    {
        private readonly Func<string, IUserservice> _factory;
       
        public fakeuserservicesameinterface(Func<string, IUserservice> factory)
        {
            _factory = factory;
           
        }
        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
            
        }

        public async Task<List<MyUser>> Getall()
        {
            //throw new NotImplementedException();
            // return await _factory("fakeuserservicesameinterface").Getall(); 
            return await _factory("fakeuserservicesameinterface").Getall();
        }

        public Task<MyUser> Getsingle(string id)
        {
            throw new NotImplementedException();
        }

        public MyUser Update(MyUser toupdate)
        {
            throw new NotImplementedException();
        }
    }
}
