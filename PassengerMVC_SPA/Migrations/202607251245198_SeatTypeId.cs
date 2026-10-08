namespace PassengerMVC_SPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeatTypeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Passengers", "SeatType_SeatTypeId", "dbo.SeatTypes");
            DropIndex("dbo.Passengers", new[] { "SeatType_SeatTypeId" });
            RenameColumn(table: "dbo.Passengers", name: "SeatType_SeatTypeId", newName: "SeatTypeId");
            AlterColumn("dbo.Passengers", "SeatTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Passengers", "SeatTypeId");
            AddForeignKey("dbo.Passengers", "SeatTypeId", "dbo.SeatTypes", "SeatTypeId", cascadeDelete: true);
            DropColumn("dbo.Passengers", "FacilityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passengers", "FacilityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Passengers", "SeatTypeId", "dbo.SeatTypes");
            DropIndex("dbo.Passengers", new[] { "SeatTypeId" });
            AlterColumn("dbo.Passengers", "SeatTypeId", c => c.Int());
            RenameColumn(table: "dbo.Passengers", name: "SeatTypeId", newName: "SeatType_SeatTypeId");
            CreateIndex("dbo.Passengers", "SeatType_SeatTypeId");
            AddForeignKey("dbo.Passengers", "SeatType_SeatTypeId", "dbo.SeatTypes", "SeatTypeId");
        }
    }
}
