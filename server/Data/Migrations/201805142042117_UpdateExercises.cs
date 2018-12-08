namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExercises : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "VideoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "VideoUrl");
        }
    }
}
