namespace Chat.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserFromId = c.Int(nullable: false),
                        UserToId = c.Int(nullable: false),
                        MessageType = c.Int(nullable: false),
                        MessageContent = c.String(),
                        LastModified = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserFromId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserToId, cascadeDelete: false)
                .Index(t => t.UserFromId)
                .Index(t => t.UserToId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        DateOfBirthday = c.DateTime(),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        LastModified = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserToId", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserFromId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "UserToId" });
            DropIndex("dbo.Messages", new[] { "UserFromId" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
