using Prestame.Data;
using Prestame.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Prestame.Models
{
    public class Prestamos : IEntity, IDisposable
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public decimal InteresInicial { get; set; }
        public decimal? InteresActual { get; set; }
        [Required]
        public decimal CapitalInicial { get; set; }
        public decimal? CapitalActual { get; set; }
        [Required]
        public int ClienteId { get; set; }
        public PrestamosEstados Estado { get; set; }
        [Required]
        public DateTime FechaDeCreacion { get; set; }
        public DateTime? FechaDeSaldo { get; set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}