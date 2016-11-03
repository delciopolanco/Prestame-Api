namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixinNamesSpaces : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrestamosEstados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false, maxLength: 200),
                        PrestamoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstadosClientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false, maxLength: 200),
                        ClienteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Prestamos", "Estado_Id", c => c.Int());
            CreateIndex("dbo.Prestamos", "Estado_Id");
            AddForeignKey("dbo.Prestamos", "Estado_Id", "dbo.PrestamosEstados", "Id");
            DropColumn("dbo.Prestamos", "Estado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prestamos", "Estado", c => c.Int(nullable: false));
            DropForeignKey("dbo.Prestamos", "Estado_Id", "dbo.PrestamosEstados");
            DropIndex("dbo.Prestamos", new[] { "Estado_Id" });
            DropColumn("dbo.Prestamos", "Estado_Id");
            DropTable("dbo.EstadosClientes");
            DropTable("dbo.PrestamosEstados");
        }
    }
}
