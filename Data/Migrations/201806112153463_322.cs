namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _322 : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.UserExercises", new[] { "Id" });
            //DropColumn("dbo.UserExercises", "ExerciseId");
            //RenameColumn(table: "dbo.UserExercises", name: "Id", newName: "ExerciseId");
            //AlterColumn("dbo.UserExercises", "ExerciseId", c => c.Int(nullable: false));
            //CreateIndex("dbo.UserExercises", "ExerciseId");
        }
        
        public override void Down()
        {
            //DropIndex("dbo.UserExercises", new[] { "ExerciseId" });
            //AlterColumn("dbo.UserExercises", "ExerciseId", c => c.Int(nullable: false, identity: true));
            //RenameColumn(table: "dbo.UserExercises", name: "ExerciseId", newName: "Id");
            //AddColumn("dbo.UserExercises", "ExerciseId", c => c.Int(nullable: false));
            //CreateIndex("dbo.UserExercises", "Id");
        }
    }
}
