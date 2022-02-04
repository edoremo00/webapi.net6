using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.Context;

namespace testidentityandjwt.DAL.Repository
{
    /* public class EFRepository<T> where T : class
     {
         private readonly jwtandidentitycontext _context;
         //private readonly DbSet<T> _table;

         public EFRepository(jwtandidentitycontext context)
         {
             _context = context;
             //_table = table;
         }

         protected jwtandidentitycontext jwtandidentitycontext => _context;
        // protected DbSet<T> table => _table;
     }*/

    public class EFRepository
    {
        private readonly jwtandidentitycontext _context;
        //private readonly DbSet<T> _table;

        public EFRepository(jwtandidentitycontext context)
        {
            _context = context;
            //_table = table;
        }

        protected jwtandidentitycontext jwtandidentitycontext => _context;
        // protected DbSet<T> table => _table;
    }
}
