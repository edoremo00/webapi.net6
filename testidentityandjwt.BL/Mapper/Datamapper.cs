using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IMapper;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.BL.IServices
{
    public class Datamapper : IDatamapper
    {
        public UserDTO mapmyusertodto(MyUser tomap)
        {
            return new UserDTO
            {
                Email = tomap.Email,
                Userid = tomap.Id,
                Birthday = tomap.birthday ?? DateTime.Now,
                Username = tomap.UserName

            };
        }

        public TodoDTO maptodototodoDTO(Todo todo)
        {
            return new TodoDTO
            {
                Created = todo.CreationDate,
                Id = todo.Id,
                Title = todo.Title,
                IsDeleted = todo.istodoDeleted,
                Description = todo.Description,
                isTodoDone = todo.isDone,
                UserId = todo.UserId,
                Lastmodified = todo.LastModifiedDate
            };
        }
    }
}
