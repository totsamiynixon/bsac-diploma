namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfessionAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Professions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfessionCriterias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Weight = c.Int(nullable: false),
                        ProfessionId = c.Int(nullable: false),
                        CriteriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Criteria", t => t.CriteriaId, cascadeDelete: true)
                .ForeignKey("dbo.Professions", t => t.ProfessionId, cascadeDelete: true)
                .Index(t => t.ProfessionId)
                .Index(t => t.CriteriaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfessionCriterias", "ProfessionId", "dbo.Professions");
            DropForeignKey("dbo.ProfessionCriterias", "CriteriaId", "dbo.Criteria");
            DropIndex("dbo.ProfessionCriterias", new[] { "CriteriaId" });
            DropIndex("dbo.ProfessionCriterias", new[] { "ProfessionId" });
            DropTable("dbo.ProfessionCriterias");
            DropTable("dbo.Professions");
        }
    }
}
