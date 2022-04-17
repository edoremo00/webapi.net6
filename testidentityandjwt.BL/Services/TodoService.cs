using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.Repository;

namespace testidentityandjwt.BL.Services
{
    public class TodoService : EFRepository, ICrudinterface<Todo, TodoDTO>,ITodoService
    {
        private readonly Datamapper _mapper;
        public TodoService(jwtandidentitycontext context, Datamapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public TodoDTO Create(TodoDTO entity)
        {
            Todo todo = new()
            {
                Title = entity.Title,
                CreationDate = DateTime.Now,
                Description = entity.Description,
                //isDone = entity.isTodoDone,
                //istodoDeleted = entity.IsDeleted,
                UserId = entity.UserId,
                LastModifiedDate = DateTime.Now,
            };
            jwtandidentitycontext.Todos.Add(todo);
            jwtandidentitycontext.SaveChanges();
            return _mapper.maptodototodoDTO(todo);
        }

        public bool Delete(int id)
        {
            Todo? todelete = jwtandidentitycontext.Todos.Find(id);
            if (todelete is not null && !todelete.istodoDeleted)
            {
                todelete.istodoDeleted = true;
                jwtandidentitycontext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<TodoDTO> GetAll()
        {
            return jwtandidentitycontext.Todos.
               AsNoTracking().
               Where(t => t.istodoDeleted == false).Select(t => _mapper.maptodototodoDTO(t));
        }

        public TodoDTO? GetById(int id)
        {
            Todo? tofind = jwtandidentitycontext.Todos.Find(id);
            if (tofind is not null && !tofind.istodoDeleted)
            {
                return _mapper.maptodototodoDTO(tofind);
            }
            return null;
        }

        public TodoDTO? Update(TodoDTO entity)
        {
            Todo? toupdtate = Genericgetsingle(entity.Id);
            if (toupdtate is not null)
            {
                toupdtate.istodoDeleted = entity.IsDeleted;
                toupdtate.Description = entity.Description;
                toupdtate.isDone = entity.isTodoDone;
                toupdtate.Title = entity.Title;
                toupdtate.LastModifiedDate = DateTime.Now;

                jwtandidentitycontext.SaveChangesAsync().GetAwaiter().GetResult();
                return _mapper.maptodototodoDTO(toupdtate);

            }
            return null;

        }

        private Todo? Genericgetsingle(int id)
        {
           Todo? t= jwtandidentitycontext.Todos.Find(id);
            if (t is null || t.istodoDeleted)
                return null;
            return t;
        }

        public IEnumerable<TodoDTO> Gettodowithusers()
        {
            return jwtandidentitycontext.Todos.AsNoTracking()
                .Where(t => t.istodoDeleted == false).
                Include(u => u.User)
                .Select(t => _mapper.maptodototodoDTO(t)).ToList();
        }

        public IEnumerable<TodoDTO> Getdonetodos(string foruserid="")
        {
            return String.IsNullOrEmpty(foruserid) ?
                  jwtandidentitycontext.Todos.AsNoTracking()
                 .Where(t => t.isDone == true && !t.istodoDeleted)
                 .Select(t => _mapper.maptodototodoDTO(t)).ToList()

                 :
                  jwtandidentitycontext.Todos.AsNoTracking()
                 .Where(t => t.isDone == true && !t.istodoDeleted && t.UserId==foruserid)
                 .Select(t => _mapper.maptodototodoDTO(t)).ToList();

        }

        public IEnumerable<TodoDTO> Getallusertodo(string foruserid,bool externalloginuser)
        {
            if (externalloginuser)
            {
                return jwtandidentitycontext.Todos.AsNoTracking()
               .Where(t => t.User.Email == foruserid && !t.istodoDeleted)
               .Select(t => _mapper.maptodototodoDTO(t));
            }
            return jwtandidentitycontext.Todos.AsNoTracking()
                .Where(t => t.UserId == foruserid && !t.istodoDeleted)
                .Select(t => _mapper.maptodototodoDTO(t));
        }

        
    }

    
}
