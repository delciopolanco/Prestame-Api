using System.ComponentModel;

namespace Prestame.Models
{
    public class Estados
    {
       public enum EstadosPrestamos
        {
            [Description("Al Día")]
            AlDía = 1,
            [Description("En Atraso")]
            EnAtraso = 2,
            [Description("En Mora")]
            EnMora = 3,
            [Description("Saldado")]
            Salsado = 4
        }
                 
    }
}