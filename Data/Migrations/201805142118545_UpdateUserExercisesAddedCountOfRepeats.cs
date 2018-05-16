namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserExercisesAddedCountOfRepeats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserExercises", "CountOfRepeats", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserExercises", "CountOfRepeats");
        }
    }
}
