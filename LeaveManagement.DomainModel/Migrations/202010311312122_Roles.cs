namespace LeaveManagement.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Roles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HrRoles",
                c => new
                    {
                        HrID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HrID)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.ProjectMangerRoles",
                c => new
                    {
                        PmID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PmID)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
            AddColumn("dbo.Leaves", "ProjectManagerID", c => c.Int(nullable: false));
            AddColumn("dbo.Leaves", "HrRole_HrID", c => c.Int());
            AddColumn("dbo.Leaves", "ProjectMangerRole_PmID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "EmployeeRoles", c => c.String());
            AddColumn("dbo.AspNetUsers", "HrRole_HrID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "HrRole_HrID");
            CreateIndex("dbo.Leaves", "HrRole_HrID");
            CreateIndex("dbo.Leaves", "ProjectMangerRole_PmID");
            AddForeignKey("dbo.AspNetUsers", "HrRole_HrID", "dbo.HrRoles", "HrID");
            AddForeignKey("dbo.Leaves", "HrRole_HrID", "dbo.HrRoles", "HrID");
            AddForeignKey("dbo.Leaves", "ProjectMangerRole_PmID", "dbo.ProjectMangerRoles", "PmID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "ProjectMangerRole_PmID", "dbo.ProjectMangerRoles");
            DropForeignKey("dbo.ProjectMangerRoles", "EmployeeID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Leaves", "HrRole_HrID", "dbo.HrRoles");
            DropForeignKey("dbo.AspNetUsers", "HrRole_HrID", "dbo.HrRoles");
            DropForeignKey("dbo.HrRoles", "EmployeeID", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectMangerRoles", new[] { "EmployeeID" });
            DropIndex("dbo.Leaves", new[] { "ProjectMangerRole_PmID" });
            DropIndex("dbo.Leaves", new[] { "HrRole_HrID" });
            DropIndex("dbo.AspNetUsers", new[] { "HrRole_HrID" });
            DropIndex("dbo.HrRoles", new[] { "EmployeeID" });
            DropColumn("dbo.AspNetUsers", "HrRole_HrID");
            DropColumn("dbo.AspNetUsers", "EmployeeRoles");
            DropColumn("dbo.Leaves", "ProjectMangerRole_PmID");
            DropColumn("dbo.Leaves", "HrRole_HrID");
            DropColumn("dbo.Leaves", "ProjectManagerID");
            DropTable("dbo.ProjectMangerRoles");
            DropTable("dbo.HrRoles");
        }
    }
}
