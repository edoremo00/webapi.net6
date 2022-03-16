using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.BL.Utils;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ICrudinterface<DAL.Entities.Todo, TodoDTO> _crudinterface;
        private readonly ITodoService _todoService;

        public TodoController(ICrudinterface<Todo,TodoDTO> crudinterface,ITodoService todoService)
        {
            _crudinterface = crudinterface;
            _todoService = todoService;
        }

        [HttpPost,Route("CreateTodo")]
        public ActionResult<TodoDTO> CreateTodo(TodoDTO todoDTO)
        {
            return Ok(_crudinterface.Create(todoDTO));

        }

        [HttpGet,Route("GetTodos")]
        public  ActionResult GetTodos()
        {
            var alltodos = _crudinterface.GetAll();
            return alltodos.Count()==0 ?  NoContent(): Ok(alltodos);
        }

        [HttpGet,Route("GetSingletodo/{id:min(0)}")]
        public ActionResult<TodoDTO> GetSingle(int id)
        {
            TodoDTO? search=_crudinterface.GetById(id);
            if (search is null)
                return NotFound(new Error { StatusCode=404,Message="todo not found"});
            return Ok(search);
        }

        [HttpGet,Route("Gettodowithusers")]
        public ActionResult<TodoDTO> Gettodowithusers()
        {
            IEnumerable<TodoDTO> todowithusers= _todoService.Gettodowithusers();
            return todowithusers.Count()==0 ? NoContent(): Ok(todowithusers);
        }

        [HttpGet,Route("Getallusertodo")]
        public ActionResult<TodoDTO> Getallusertodo(string foruserid)
        {
            IEnumerable<TodoDTO> allusertodo = _todoService.Getallusertodo(foruserid);
            return allusertodo.Count()==0 ? NoContent() : Ok(allusertodo);
        }

        [HttpGet,Route("Getdonetodos")]
        public ActionResult<TodoDTO> Getdonetodos(string? foruserid="")
        {
          IEnumerable<TodoDTO> donetods= _todoService.Getdonetodos(foruserid);
          return donetods.Count()==0 ? NoContent() : Ok(donetods);
        }

        [HttpPut,Route("Updatetodo")]
        public ActionResult<TodoDTO> Updatetodo(TodoDTO toupdate)
        {
            TodoDTO? isupdated = _crudinterface.Update(toupdate);
            return isupdated is null? UnprocessableEntity(new Error { StatusCode=422,Message=$"unable to update todo with id: {toupdate.Id}"}) : Ok(isupdated);
        }

        [HttpDelete,Route("DeleteTodo/{id:min(0)}")]
        public ActionResult<bool> DeleteTodo(int id)
        {
           
           return _crudinterface.Delete(id) ?  Ok(JsonSerializer.Serialize(true)) :NotFound(new Error { StatusCode=404,Message="todo not found"});
        }
    }
}
