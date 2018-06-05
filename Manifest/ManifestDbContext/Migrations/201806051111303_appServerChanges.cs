namespace ManifestDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appServerChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppServers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Uri = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppServers");
        }
    }
}
