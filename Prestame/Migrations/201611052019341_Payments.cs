namespace Prestame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pagos", "Cliente_Id", c => c.Int());
            CreateIndex("dbo.Pagos", "Cliente_Id");
            AddForeignKey("dbo.Pagos", "Cliente_Id", "dbo.Cliente", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pagos", "Cliente_Id", "dbo.Cliente");
            DropIndex("dbo.Pagos", new[] { "Cliente_Id" });
            DropColumn("dbo.Pagos", "Cliente_Id");
        }
    }
}
