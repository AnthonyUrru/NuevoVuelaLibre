using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VuelaLibre.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [MinLength(2)]
        [RegularExpression(@"[a-zA-Z]+")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo apellido es requerido.")]
        [MinLength(2)]
        [RegularExpression(@"[a-zA-Z]+")]
        public string Apellido { get; set; }
        [Required(ErrorMessage ="El campo correo es requerido.")]
        [EmailAddress]
        public string Correo { get; set; }
        [Required(ErrorMessage ="El campo contraseña como mínimo debe contener al menos 6 caracteres.")]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
        [Required(ErrorMessage = "El campo Confirmar contraseña es obligatorio.")]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string VerfContraseña { get; set; }
        public decimal saldo { get; set; }
    }
}
