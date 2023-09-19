namespace Heuristics.TechEval.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMemberCategory : DbMigration
    {
        public override void Up()
        {
            // defaults to system (0), but records will start at 1
            // hacky way to hope we get a default category
            AddColumn("dbo.Member", "CategoryId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Member", "CategoryId");
            AddForeignKey("dbo.Member", "CategoryId", "dbo.Category", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Member", "CategoryId", "dbo.Category");
            DropIndex("dbo.Member", new[] { "CategoryId" });
            DropColumn("dbo.Member", "CategoryId");
        }
    }
}
