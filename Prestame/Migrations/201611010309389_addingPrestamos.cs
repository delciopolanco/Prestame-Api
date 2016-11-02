namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addingPrestamos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prestamos",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    InteresInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                    InteresActual = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CapitalInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CapitalActual = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ClienteId = c.Int(),
                    Estado = c.Int(nullable: false, defaultValue: 1),
                    FechaDeCreacion = c.DateTime(nullable: false),
                    FechaDeSaldo = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Prestamos", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Prestamos", new[] { "ClienteId" });
            DropTable("dbo.Prestamos");
        }
    }
}
