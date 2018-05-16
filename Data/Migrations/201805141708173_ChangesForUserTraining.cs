namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesForUserTraining : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTrainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsPassed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserExercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserTrainingId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .ForeignKey("dbo.UserTrainings", t => t.UserTrainingId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ExerciseId)
                .Index(t => t.UserTrainingId);
            
            AddColumn("dbo.Exercises", "DifficultyLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTrainings", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserExercises", "UserTrainingId", "dbo.UserTrainings");
            DropForeignKey("dbo.UserExercises", "Id", "dbo.Exercises");
            DropIndex("dbo.UserExercises", new[] { "UserTrainingId" });
            DropIndex("dbo.UserExercises", new[] { "Id" });
            DropIndex("dbo.UserTrainings", new[] { "UserId" });
            DropColumn("dbo.Exercises", "DifficultyLevel");
            DropTable("dbo.UserExercises");
            DropTable("dbo.UserTrainings");
        }
    }
}
