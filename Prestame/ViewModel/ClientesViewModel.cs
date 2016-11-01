using System.Collections.Generic;
using Prestame.Models;
using System;
using Prestame.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prestame.Models
{
    public class ClienteViewModel : IEntity, IDisposable
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "* campo nombres Requerido")]
        [MaxLength(50, ErrorMessage = "* El campo Nombres no puede ser mayor a 50 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "* Campo apellidos Requerido")]
        [MaxLength(50, ErrorMessage = "* El campo Apellidos no puede ser mayor a 50 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "* Campo identificación Requerido")]
        [MaxLength(15, ErrorMessage = "* El campo Identificación no puede ser mayor a 15 caracteres")]
        public string Identificacion { get; set; }

        [JsonIgnore]
        public ICollection<TelefonosViewModel> Telefonos { get; set; }

        [JsonIgnore]
        public ICollection<Direcciones> Direcciones { get; set; }

        [JsonIgnore]
        public ICollection<Prestamos> Prestamos { get; set; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}