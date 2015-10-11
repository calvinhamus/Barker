namespace Barker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rebarks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Barks", "Rebarks", c => c.Int());
        }
        
        public override void Down()
        {
        }
    }
}
