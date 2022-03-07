using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.BL.Services
{
    public class TodoService :EFRepository, ICrudinterface<TodoService>
    {
        public TodoService(jwtandidentitycontext context) : base(context)
        {
        }

        public Task<TodoService> CreateAsync(TodoService entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TodoService> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TodoService> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TodoService> UpdateAsync(TodoService entity)
        {
            throw new NotImplementedException();
        }
    }
}
