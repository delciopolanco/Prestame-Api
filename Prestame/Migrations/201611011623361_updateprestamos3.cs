namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateprestamos3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prestamos", "InteresActual", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Prestamos", "CapitalActual", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Prestamos", "Estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prestamos", "Estado", c => c.Int());
            AlterColumn("dbo.Prestamos", "CapitalActual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prestamos", "InteresActual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
