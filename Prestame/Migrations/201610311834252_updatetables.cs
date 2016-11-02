namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetables : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Direcciones", name: "Cliente_Id", newName: "ClienteId");
            RenameColumn(table: "dbo.Telefonos", name: "Cliente_Id", newName: "ClienteId");
            RenameIndex(table: "dbo.Direcciones", name: "IX_Cliente_Id", newName: "IX_ClienteId");
            RenameIndex(table: "dbo.Telefonos", name: "IX_Cliente_Id", newName: "IX_ClienteId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Telefonos", name: "IX_ClienteId", newName: "IX_Cliente_Id");
            RenameIndex(table: "dbo.Direcciones", name: "IX_ClienteId", newName: "IX_Cliente_Id");
            RenameColumn(table: "dbo.Telefonos", name: "ClienteId", newName: "Cliente_Id");
            RenameColumn(table: "dbo.Direcciones", name: "ClienteId", newName: "Cliente_Id");
        }
    }
}
