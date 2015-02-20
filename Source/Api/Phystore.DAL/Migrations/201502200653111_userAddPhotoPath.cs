namespace Phystore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userAddPhotoPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PhotoPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PhotoPath");
        }
    }
}
