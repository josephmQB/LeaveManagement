namespace LeaveManagement.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leaves", "ProjectMangerRole_PmID", "dbo.ProjectMangerRoles");
            DropIndex("dbo.Leaves", new[] { "ProjectMangerRole_PmID" });
            DropColumn("dbo.Leaves", "ProjectManagerID");
            RenameColumn(table: "dbo.Leaves", name: "ProjectMangerRole_PmID", newName: "ProjectManagerID");
            AlterColumn("dbo.Leaves", "ProjectManagerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Leaves", "ProjectManagerID");
            AddForeignKey("dbo.Leaves", "ProjectManagerID", "dbo.ProjectMangerRoles", "PmID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "ProjectManagerID", "dbo.ProjectMangerRoles");
            DropIndex("dbo.Leaves", new[] { "ProjectManagerID" });
            AlterColumn("dbo.Leaves", "ProjectManagerID", c => c.Int());
            RenameColumn(table: "dbo.Leaves", name: "ProjectManagerID", newName: "ProjectMangerRole_PmID");
            AddColumn("dbo.Leaves", "ProjectManagerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Leaves", "ProjectMangerRole_PmID");
            AddForeignKey("dbo.Leaves", "ProjectMangerRole_PmID", "dbo.ProjectMangerRoles", "PmID");
        }
    }
}
