using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.BL.Interfaces
{
    public interface IDigitaltodo//se todo include media(foto,audio)
    {
        public string? Filename { get; set; }
        public DateTime Uploaddate { get; set; }
        //metodi per caricare file,cancellarli ecc
    }
}
