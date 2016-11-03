using Prestame.Data;
using Prestame.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Prestame.Models
{
    public class Cliente : IEntity,IDisposable
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombres { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellidos { get; set; }
        [Required]
        [StringLength(15)]
        public string Identificacion { get; set; }
        public virtual ICollection<Direcciones> Direcciones { get; set; }

        public virtual ICollection<Telefonos> Telefonos { get; set; }

        public virtual ICollection<Prestamos> Prestamos { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}