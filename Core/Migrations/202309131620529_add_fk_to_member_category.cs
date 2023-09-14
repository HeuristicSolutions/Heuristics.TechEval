namespace Heuristics.TechEval.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_fk_to_member_category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "CategoryId", c => c.Int(nullable: false));
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
