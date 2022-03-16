using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.BL.DTO;

namespace testidentityandjwt.BL.IServices
{
    public interface ITodoService
    {
        public IEnumerable<DTO.TodoDTO> Gettodowithusers();
        public IEnumerable<TodoDTO> Getdonetodos(string foruserid = "");
        public IEnumerable<TodoDTO> Getallusertodo(string foruserid);
    }
}
