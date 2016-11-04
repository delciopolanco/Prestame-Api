using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prestame.ViewModel
{
    public class PagosViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Capital es Requerido")]
        public decimal Capital { get; set; }
        [Required(ErrorMessage = "* Interes es Requerido")]
        public decimal Interes { get; set; }
        [Required(ErrorMessage = "* Tasa es Requerido")]
        public decimal Tasa { get; set; }
        public DateTime FechaPago { get; set; }
        public int? PrestamoId { get; set; }
    }
}