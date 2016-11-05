using Prestame.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prestame.ViewModel
{
    public class PagosViewModel : IEntity, IDisposable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "* Monto Pago es Requerido")]
        public decimal Monto { get; set; }
        public decimal? Interes { get; set; }
        [Range(0.01, 999, ErrorMessage = "* La Tasa debe ser mayor a 0.01 y menor a 999")]
        public decimal? Tasa { get; set; }
        public decimal? Capital { get; set; }
        public DateTime FechaPago { get; set; }

        [Required(ErrorMessage = "* Id del prestamo Requerido")]
        public int? PrestamoId { get; set; }
        public string NombreCliente { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}