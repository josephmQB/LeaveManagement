namespace LeaveManagement.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSpecialPermission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HrRoles", "IsSpecialPermission", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HrRoles", "IsSpecialPermission");
        }
    }
}
