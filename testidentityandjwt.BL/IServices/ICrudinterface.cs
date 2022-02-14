using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.BL.IServices
{
    public interface ICrudinterface<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> Delete(int id);
        Task<T> GetById(int id);
        IQueryable<T> GetAll();
    }
}
