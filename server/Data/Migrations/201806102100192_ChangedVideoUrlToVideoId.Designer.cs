// <auto-generated />
namespace Data.Implementations.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class ChangedVideoUrlToVideoId : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ChangedVideoUrlToVideoId));
        
        string IMigrationMetadata.Id
        {
            get { return "201806102100192_ChangedVideoUrlToVideoId"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
