namespace Lab4University.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chair",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Institute_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Institute", t => t.Institute_ID)
                .Index(t => t.Institute_ID);
            
            CreateTable(
                "dbo.Institute",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PIB = c.String(nullable: false, maxLength: 50, unicode: false),
                        Chair_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Chair", t => t.Chair_ID)
                .Index(t => t.Chair_ID);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Hour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 15, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PIB = c.String(nullable: false, maxLength: 50, unicode: false),
                        Group_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Group", t => t.Group_ID)
                .Index(t => t.Group_ID);
            
            CreateTable(
                "dbo.GroupSubject",
                c => new
                    {
                        Group_ID = c.Int(nullable: false),
                        Subject_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_ID, t.Subject_ID })
                .ForeignKey("dbo.Group", t => t.Group_ID, cascadeDelete: true)
                .ForeignKey("dbo.Subject", t => t.Subject_ID, cascadeDelete: true)
                .Index(t => t.Group_ID)
                .Index(t => t.Subject_ID);
            
            CreateTable(
                "dbo.TeacherSubject",
                c => new
                    {
                        Subject_ID = c.Int(nullable: false),
                        Teacher_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_ID, t.Teacher_ID })
                .ForeignKey("dbo.Subject", t => t.Subject_ID, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.Teacher_ID, cascadeDelete: true)
                .Index(t => t.Subject_ID)
                .Index(t => t.Teacher_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teacher", "Chair_ID", "dbo.Chair");
            DropForeignKey("dbo.TeacherSubject", "Teacher_ID", "dbo.Teacher");
            DropForeignKey("dbo.TeacherSubject", "Subject_ID", "dbo.Subject");
            DropForeignKey("dbo.GroupSubject", "Subject_ID", "dbo.Subject");
            DropForeignKey("dbo.GroupSubject", "Group_ID", "dbo.Group");
            DropForeignKey("dbo.Student", "Group_ID", "dbo.Group");
            DropForeignKey("dbo.Chair", "Institute_ID", "dbo.Institute");
            DropIndex("dbo.TeacherSubject", new[] { "Teacher_ID" });
            DropIndex("dbo.TeacherSubject", new[] { "Subject_ID" });
            DropIndex("dbo.GroupSubject", new[] { "Subject_ID" });
            DropIndex("dbo.GroupSubject", new[] { "Group_ID" });
            DropIndex("dbo.Student", new[] { "Group_ID" });
            DropIndex("dbo.Teacher", new[] { "Chair_ID" });
            DropIndex("dbo.Chair", new[] { "Institute_ID" });
            DropTable("dbo.TeacherSubject");
            DropTable("dbo.GroupSubject");
            DropTable("dbo.Student");
            DropTable("dbo.Group");
            DropTable("dbo.Subject");
            DropTable("dbo.Teacher");
            DropTable("dbo.Institute");
            DropTable("dbo.Chair");
        }
    }
}
