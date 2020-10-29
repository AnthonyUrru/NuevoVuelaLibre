using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace VuelaLibre.Models
{
    public class Vuelo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo origen es obligatorio")]
        public string Origen { get; set; }
        [Required(ErrorMessage = "El campo destinos es obligatorio")]
        public string Destino { get; set; }
        //[Required(ErrorMessage = "El campo aerolinea es obligatorio")]
        public string Aerolinea { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public DateTime FechaIda { get; set; }
       // [Required(ErrorMessage = "El campo tiempo de vuelo es obligatorio")]
        public int TiempoVuelo { get; set; }
      //  [Required(ErrorMessage = "El campo total pasajes es obligatorio")]
        public int TotalPasaje { get; set; }
       // [Required(ErrorMessage = "El campo precio es obligatorio")]
        public decimal Precio { get; set; }
    }
}
