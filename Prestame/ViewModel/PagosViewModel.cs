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
        [Required(ErrorMessage = "* Monto Total es Requerido")]
        public decimal MontoTotal { get; set; }
        public decimal? Interes { get; set; }
        public decimal? Tasa { get; set; }
        public decimal? Capital { get; set; }
        public DateTime FechaPago { get; set; }

        [Required(ErrorMessage = "* Id del prestamo Reuqerido")]
        public int? PrestamoId { get; set; }
        public string NombreCliente { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}