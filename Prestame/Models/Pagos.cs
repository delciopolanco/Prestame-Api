using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Prestame.Models
{
    public class Pagos
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public decimal Monto { get; set; }
        public decimal? Interes { get; set; }
        public decimal? Tasa { get; set; }
        public decimal? Capital { get; set; }
        [Required]
        public DateTime FechaPago { get; set; }
        [Required]
        public int? PrestamoId { get; set; }

    }
}
