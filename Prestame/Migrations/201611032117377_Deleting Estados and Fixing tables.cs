namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingEstadosandFixingtables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prestamos", "Estado_Id", "dbo.PrestamosEstados");
            DropIndex("dbo.Prestamos", new[] { "Estado_Id" });
            AddColumn("dbo.Prestamos", "Estado", c => c.Int(nullable: false));
            DropColumn("dbo.Prestamos", "Estado_Id");
            DropTable("dbo.PrestamosEstados");
            DropTable("dbo.EstadosClientes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EstadosClientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrestamosEstados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Prestamos", "Estado_Id", c => c.Int());
            DropColumn("dbo.Prestamos", "Estado");
            CreateIndex("dbo.Prestamos", "Estado_Id");
            AddForeignKey("dbo.Prestamos", "Estado_Id", "dbo.PrestamosEstados", "Id");
        }
    }
}
