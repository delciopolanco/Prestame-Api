using Prestame.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Prestame.Data
{
    public class PrestameContext: DbContext
    {
        public PrestameContext(): base("Prestame")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            //Database.SetInitializer<PrestameContext>(new DropCreateDatabaseAlways<PrestameContext>());
            //Database.SetInitializer(new PrestameInitializer());
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Direcciones> Direcciones { get; set; }
        public DbSet<Telefonos> Telefonos { get; set; }
        public DbSet<TiposTelefono> TiposTelefono { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(mb);
        }
    }
}