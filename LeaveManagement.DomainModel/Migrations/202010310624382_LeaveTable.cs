namespace LeaveManagement.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeaveTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        NoOfDays = c.Int(nullable: false),
                        LeaveStatus = c.String(),
                        LeaveDescription = c.String(),
                        EmployeeID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LeaveID)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "EmployeeID", "dbo.AspNetUsers");
            DropIndex("dbo.Leaves", new[] { "EmployeeID" });
            DropTable("dbo.Leaves");
        }
    }
}
