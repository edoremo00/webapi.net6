using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.BL.Services
{
    public class fakeuserservice
    {
        //CLASSE CREATA PER CAPIRE COSA SUCCEDE NEL CASO IN CUI IMPLEMENTO STESSA INTERFACCIA IMPLEMENTATA ALTROVE IN ALTRA CLASSE
        /* public class fakeuserservice : Userservice//eredita da classe userservice. metodo in classe userservice deve essere virtual per poter fare override qui
         {
             public fakeuserservice(jwtandidentitycontext context) : base(context)
             {
             }

             public override async Task<List<MyUser>> Getall() //eseguo override classe madre senza chiamaerebbe il metodo della classe madre e non questo
             {
                 return await jwtandidentitycontext.Users.ToListAsync();
             }


         }*/
    }
}
