namespace Heuristics.TechEval.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ModifiedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Member", "Name", c => c.String(maxLength: 80));
            AlterColumn("dbo.Member", "Email", c => c.String(maxLength: 80));
        }

        public override void Down()
        {
            AlterColumn("dbo.Member", "Email", c => c.String());
            AlterColumn("dbo.Member", "Name", c => c.String());
        }
    }
}
