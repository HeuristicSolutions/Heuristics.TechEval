﻿namespace Heuristics.TechEval.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checktwo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Member", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Member", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Member", "Email", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Member", "Name", c => c.String(nullable: false, maxLength: 80));
        }
    }
}
