namespace ActiveCitizenWeb.DataAccess.Migrations.ApplicationIdentity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "auth.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "auth.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("auth.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("auth.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "auth.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsLdapUser = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "auth.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("auth.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "auth.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("auth.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("auth.AspNetUserRoles", "UserId", "auth.AspNetUsers");
            DropForeignKey("auth.AspNetUserLogins", "UserId", "auth.AspNetUsers");
            DropForeignKey("auth.AspNetUserClaims", "UserId", "auth.AspNetUsers");
            DropForeignKey("auth.AspNetUserRoles", "RoleId", "auth.AspNetRoles");
            DropIndex("auth.AspNetUserLogins", new[] { "UserId" });
            DropIndex("auth.AspNetUserClaims", new[] { "UserId" });
            DropIndex("auth.AspNetUsers", "UserNameIndex");
            DropIndex("auth.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("auth.AspNetUserRoles", new[] { "UserId" });
            DropIndex("auth.AspNetRoles", "RoleNameIndex");
            DropTable("auth.AspNetUserLogins");
            DropTable("auth.AspNetUserClaims");
            DropTable("auth.AspNetUsers");
            DropTable("auth.AspNetUserRoles");
            DropTable("auth.AspNetRoles");
        }
    }
}
