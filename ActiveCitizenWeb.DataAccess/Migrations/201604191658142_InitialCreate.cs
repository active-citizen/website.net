namespace ActiveCitizenWeb.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaqListCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FaqListItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        Order = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FaqListCategories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FaqListItems", "Category_Id", "dbo.FaqListCategories");
            DropIndex("dbo.FaqListItems", new[] { "Category_Id" });
            DropTable("dbo.FaqListItems");
            DropTable("dbo.FaqListCategories");
        }
    }
}
