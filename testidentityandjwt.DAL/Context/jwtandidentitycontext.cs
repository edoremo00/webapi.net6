using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.DAL.Context
{
    public class jwtandidentitycontext:IdentityDbContext<MyUser>
    {
        //public jwtandidentitycontext() : base() { }
        public jwtandidentitycontext(DbContextOptions<jwtandidentitycontext> options) : base(options) { } //richiamo costruttore classe base ossia identity dbcontext

  
        public DbSet<MyUser> Utenti { get; set;}

      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("DefaultConnection");
        }



    }
}
