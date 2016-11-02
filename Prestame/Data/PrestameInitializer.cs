using Prestame.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Prestame.Data
{
    public class PrestameInitializer : DropCreateDatabaseAlways<PrestameContext>
    {
        public void InitDatabase(PrestameContext context)
        {
            #region "Tipos de Telefonos Default"
            IList<TiposTelefono> tiposTelefonos = new List<TiposTelefono>();

            tiposTelefonos.Add(new TiposTelefono() { TipoTelefono = "Hogar" });
            tiposTelefonos.Add(new TiposTelefono() { TipoTelefono = "Empleo" });
            tiposTelefonos.Add(new TiposTelefono() { TipoTelefono = "Movil" });

            bool existTiposTelefono = (context.TiposTelefono.ToList().Count > 0 ? true : false);
            if (!existTiposTelefono)
            {
                foreach (TiposTelefono tipo in tiposTelefonos)
                {
                    context.TiposTelefono.Add(tipo);
                }
            }
            #endregion

            #region "Adding some clientes"

            IList<Cliente> clientes = new List<Cliente>();

            clientes.Add(new Cliente() { Nombres = "Delcio Manuel", Apellidos = "Polanco Padilla", Identificacion = "001-1858957-1" });
            clientes.Add(new Cliente() { Nombres = "Dorka", Apellidos = "Mojica", Identificacion = "001-1858957-2" });
            clientes.Add(new Cliente() { Nombres = "Esther", Apellidos = "Esther", Identificacion = "001-1858957-3" });

            bool existCliente = (context.Cliente.ToList().Count > 0 ? true : false);
            if (!existCliente)
            {
                foreach (Cliente cliente in clientes)
                {
                    context.Cliente.Add(cliente);
                }
            }
            #endregion



            base.Seed(context);
        }
    }
}