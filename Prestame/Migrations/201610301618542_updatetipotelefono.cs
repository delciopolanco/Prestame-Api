namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetipotelefono : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Telefonos", name: "TiposTelefono_Id", newName: "TiposTelefonoId");
            RenameIndex(table: "dbo.Telefonos", name: "IX_TiposTelefono_Id", newName: "IX_TiposTelefonoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Telefonos", name: "IX_TiposTelefonoId", newName: "IX_TiposTelefono_Id");
            RenameColumn(table: "dbo.Telefonos", name: "TiposTelefonoId", newName: "TiposTelefono_Id");
        }
    }
}
