using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VuelaLibre.Models
{
    public class DetalleVuelo
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }

        public DateTime FechaSalida { get; set; }
        public decimal precio { get; set; }
        public int Nasiento { get; set; }
        public int UserId { get; set; }
        public string Aerolinea { get; set; }
      

    }
}
