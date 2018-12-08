namespace Data.Implementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ChangedVideoUrlToVideoId : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Exercises", "VideoUrl", "VideoId");
        }

        public override void Down()
        {
            RenameColumn("dbo.Exercises", "VideoId", "VideoUrl");
        }
    }
}
