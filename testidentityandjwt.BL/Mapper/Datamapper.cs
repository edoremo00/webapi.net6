using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IMapper;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.BL.IServices
{
    public class Datamapper<T,U> where T:class where U:class, IDatamapper<T, U> ,new()
    {
        public U Map(T entity)
        {
            return new();
        }

        
    }
}
