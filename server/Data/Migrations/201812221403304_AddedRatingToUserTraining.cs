namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRatingToUserTraining : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTrainings", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTrainings", "Rating");
        }
    }
}
