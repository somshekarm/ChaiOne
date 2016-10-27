namespace ManifestDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManifestUnitOfWork : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        File = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        ImageableId = c.Int(nullable: false),
                        ImageFileType = c.Int(nullable: false),
                        SealId = c.Guid(nullable: false),
                        Uri = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Seals", t => t.SealId, cascadeDelete: true)
                .Index(t => t.SealId);
            
            CreateTable(
                "dbo.Manifests",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OfficerName = c.String(),
                        OriginLocation = c.String(),
                        ReceivingLocation = c.String(),
                        EstimatedTimeofArrival = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        SiteId = c.Int(nullable: false),
                        ManifestStatus = c.Int(nullable: false),
                        Name = c.String(),
                        Uri = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Seals",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SealNumber = c.String(),
                        SealType = c.String(),
                        Note = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        ManifestId = c.Guid(nullable: false),
                        SealStatus = c.Int(nullable: false),
                        ArchiveStatus = c.Boolean(nullable: false),
                        Uri = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Manifests", t => t.ManifestId, cascadeDelete: true)
                .Index(t => t.ManifestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seals", "ManifestId", "dbo.Manifests");
            DropForeignKey("dbo.Images", "SealId", "dbo.Seals");
            DropIndex("dbo.Seals", new[] { "ManifestId" });
            DropIndex("dbo.Images", new[] { "SealId" });
            DropTable("dbo.Seals");
            DropTable("dbo.Manifests");
            DropTable("dbo.Images");
        }
    }
}
