namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Settings", "Id", "dbo.Users");
            AddForeignKey("dbo.Settings", "Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Settings", "Id", "dbo.Users");
            AddForeignKey("dbo.Settings", "Id", "dbo.Users", "Id");
        }
    }
}
