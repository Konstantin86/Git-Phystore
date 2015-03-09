using System.Data.Entity.Migrations;

namespace Keepfit.DAL.Migrations
{
  public partial class ChangedRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
