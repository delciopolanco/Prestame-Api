namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class defaultValuePrestamos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prestamos", "Estado", c => c.Int(nullable: false, identity: false, defaultValue: 1));
        }
        
        public override void Down()
        {
        }
    }
}

