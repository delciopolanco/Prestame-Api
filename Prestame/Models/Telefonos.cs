using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestame.Interfaces
{
    public class Telefonos
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefono  { get; set; }

        public int? TiposTelefonoId { get; set; }
        public virtual TiposTelefono TiposTelefono { get; set; }

        public int? ClienteId { get; set; }
    }
}