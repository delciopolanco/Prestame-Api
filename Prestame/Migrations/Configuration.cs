namespace Prestame.Migrations
{
    using Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Prestame.Data.PrestameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Prestame.Data.PrestameContext context)
        {
            new PrestameInitializer().InitDatabase(context);
        }
    }
}
