namespace DBQueryTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReportFile_Name = c.String(),
                        ReportFile_TransactionContext = c.Binary(),
                        ReportFile_Position = c.Long(nullable: false),
                        ReportFile_ReadTimeout = c.Int(nullable: false),
                        ReportFile_WriteTimeout = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TemplateId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TemplateFile_Name = c.String(),
                        TemplateFile_TransactionContext = c.Binary(),
                        TemplateFile_Position = c.Long(nullable: false),
                        TemplateFile_ReadTimeout = c.Int(nullable: false),
                        TemplateFile_WriteTimeout = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TemplateTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FileExtension = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.TemplateTypes");
            DropTable("dbo.Templates");
            DropTable("dbo.Reports");
        }
    }
}
