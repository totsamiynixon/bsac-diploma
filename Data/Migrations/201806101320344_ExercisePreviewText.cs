namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExercisePreviewText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "PreviewText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "PreviewText");
        }
    }
}
