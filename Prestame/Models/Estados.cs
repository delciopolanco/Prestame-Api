using Prestame.Data;
using System;
using System.ComponentModel;

namespace Prestame.Models
{
    [Description("Lista de los Estados de Prestamos")]
    public enum PrestamoEstado
    {
        [Description("Prestamo Activo")]
        Activo = 1,
        [Description("Prestamo Saldado")]
        Saldado = 2,
        [Description("Prestamo En Atraso")]
        EnAtraso = 3,
        [Description("Prestamo En Mora")]
        EnMora = 4
    }

    [Description("Lista de los Estados de Clientes")]
    public enum ClienteEstado
    {
        [Description("Ciente Activo")]
        Activo = 1,
        [Description("Ciente Inactivo")]
        Inactivo = 2,
        [Description("Ciente Eliminado")]
        Eliminado = 3
    }
}