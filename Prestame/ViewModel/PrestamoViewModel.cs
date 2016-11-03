using Newtonsoft.Json;
using Prestame.Data;
using Prestame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prestame.Helpers
{
    public class PrestamoViewModel : IEntity, IDisposable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Interes inicial es Requerido")]
        [Range(0, 9999999999999999.99, ErrorMessage = "* El Interes inical debe ser mayor a 0 y menor a 9999999999999999.99")]
        public decimal InteresInicial { get; set; }
        public decimal? InteresActual { get; set; }

        [Required(ErrorMessage = "* Capital inicial es Requerido")]
        [Range(0, 9999999999999999.99, ErrorMessage = "* El Capital inical debe ser mayor a 0 y menor a 9999999999999999.99")]
        public decimal CapitalInicial { get; set; }
        public decimal? CapitalActual { get; set; }
        public int Estado { get; set; }
        public string EstadoPrestamo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime? FechaDeSaldo { get; set; }

        [Required(ErrorMessage = "* El cliente es Requerido")]
        public int ClienteId { get; set; }
        public ClienteViewModel Cliente { get; set; }

        public string NombreCliente { get; set; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}