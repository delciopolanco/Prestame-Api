using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prestame.Helpers
{
    public class PrestamosEstadosViewModel
    {
        [Required(ErrorMessage = "* Estado es Requerido")]
        public int Id { get; set; }
    }
}