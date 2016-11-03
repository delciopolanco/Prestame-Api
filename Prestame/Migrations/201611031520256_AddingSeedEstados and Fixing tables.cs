namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSeedEstadosandFixingtables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PrestamosEstados", "PrestamoId");
            DropColumn("dbo.EstadosClientes", "ClienteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EstadosClientes", "ClienteId", c => c.Int());
            AddColumn("dbo.PrestamosEstados", "PrestamoId", c => c.Int());
        }
    }
}
