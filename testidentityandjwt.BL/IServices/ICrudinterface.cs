using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.BL.IServices
{
    public interface ICrudinterface<T,U> where T : class where U : class
    {
        U Create(U entity);
        U? Update(U entity);
        bool Delete(int id);
        U? GetById(int id);
        IEnumerable<U> GetAll();
    }
}
