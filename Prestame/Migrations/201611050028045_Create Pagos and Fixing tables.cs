namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePagosandFixingtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pagos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Interes = c.Decimal(precision: 18, scale: 2),
                        Tasa = c.Decimal(precision: 18, scale: 2),
                        Capital = c.Decimal(precision: 18, scale: 2),
                        FechaPago = c.DateTime(nullable: false),
                        PrestamoId = c.Int(nullable: false),
                        Prestamos_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prestamos", t => t.Prestamos_Id)
                .Index(t => t.Prestamos_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pagos", "Prestamos_Id", "dbo.Prestamos");
            DropIndex("dbo.Pagos", new[] { "Prestamos_Id" });
            DropTable("dbo.Pagos");
        }
    }
}
