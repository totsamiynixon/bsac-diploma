namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExercisesAndCriterias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Criteria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExerciseCriterias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Weight = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                        CriteriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Criteria", t => t.CriteriaId, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .Index(t => t.ExerciseId)
                .Index(t => t.CriteriaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseCriterias", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.ExerciseCriterias", "CriteriaId", "dbo.Criteria");
            DropIndex("dbo.ExerciseCriterias", new[] { "CriteriaId" });
            DropIndex("dbo.ExerciseCriterias", new[] { "ExerciseId" });
            DropTable("dbo.ExerciseCriterias");
            DropTable("dbo.Exercises");
            DropTable("dbo.Criteria");
        }
    }
}
