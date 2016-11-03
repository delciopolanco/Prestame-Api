using Prestame.Data;
using Prestame.Helpers;
using System;

namespace Prestame.Helpers
{
    public class TelefonosViewModel : IEntity, IDisposable
    {

        public int? Id { get; set; }

        public string Telefono { get; set; }

        public string TiposTelefono { get; set; }

        public int? TiposTelefonoId { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}