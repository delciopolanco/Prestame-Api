namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePrestamos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prestamos", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Prestamos", new[] { "ClienteId" });
            AlterColumn("dbo.Prestamos", "InteresActual", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Prestamos", "CapitalActual", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Prestamos", "ClienteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Prestamos", "Estado", c => c.Int());
            AlterColumn("dbo.Prestamos", "FechaDeSaldo", c => c.DateTime());
            CreateIndex("dbo.Prestamos", "ClienteId");
            AddForeignKey("dbo.Prestamos", "ClienteId", "dbo.Cliente", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prestamos", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Prestamos", new[] { "ClienteId" });
            AlterColumn("dbo.Prestamos", "FechaDeSaldo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Prestamos", "Estado", c => c.Int(nullable: false));
            AlterColumn("dbo.Prestamos", "ClienteId", c => c.Int());
            AlterColumn("dbo.Prestamos", "CapitalActual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prestamos", "InteresActual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Prestamos", "ClienteId");
            AddForeignKey("dbo.Prestamos", "ClienteId", "dbo.Cliente", "Id");
        }
    }
}
