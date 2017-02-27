namespace GridViewWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GridTypes",
                c => new
                    {
                        GridTypeId = c.Short(nullable: false),
                        GridTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GridTypeId);
            
            CreateTable(
                "dbo.GridView",
                c => new
                    {
                        GridTypeId = c.Short(nullable: false),
                        ViewName = c.String(nullable: false, maxLength: 256),
                        ColumnLayout = c.String(nullable: false),
                        FilterDefinition = c.String(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsShared = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GridTypeId, t.ViewName })
                .ForeignKey("dbo.GridTypes", t => t.GridTypeId, cascadeDelete: true)
                .Index(t => t.GridTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GridView", "GridTypeId", "dbo.GridTypes");
            DropIndex("dbo.GridView", new[] { "GridTypeId" });
            DropTable("dbo.GridView");
            DropTable("dbo.GridTypes");
        }
    }
}
