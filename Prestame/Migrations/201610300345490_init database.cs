namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombres = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false, maxLength: 50),
                        Identificacion = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Direcciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Direccion = c.String(nullable: false, maxLength: 200),
                        Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.Cliente_Id)
                .Index(t => t.Cliente_Id);
            
            CreateTable(
                "dbo.Telefonos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Telefono = c.String(nullable: false, maxLength: 15),
                        TiposTelefono_Id = c.Int(),
                        Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TiposTelefono", t => t.TiposTelefono_Id)
                .ForeignKey("dbo.Cliente", t => t.Cliente_Id)
                .Index(t => t.TiposTelefono_Id)
                .Index(t => t.Cliente_Id);
            
            CreateTable(
                "dbo.TiposTelefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoTelefono = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Telefonos", "Cliente_Id", "dbo.Cliente");
            DropForeignKey("dbo.Telefonos", "TiposTelefono_Id", "dbo.TiposTelefono");
            DropForeignKey("dbo.Direcciones", "Cliente_Id", "dbo.Cliente");
            DropIndex("dbo.Telefonos", new[] { "Cliente_Id" });
            DropIndex("dbo.Telefonos", new[] { "TiposTelefono_Id" });
            DropIndex("dbo.Direcciones", new[] { "Cliente_Id" });
            DropTable("dbo.TiposTelefono");
            DropTable("dbo.Telefonos");
            DropTable("dbo.Direcciones");
            DropTable("dbo.Cliente");
        }
    }
}
