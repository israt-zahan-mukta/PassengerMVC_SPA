namespace PassengerMVC_SPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        FacilityId = c.Int(nullable: false, identity: true),
                        PassengerId = c.Int(nullable: false),
                        FacilityName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.FacilityId)
                .ForeignKey("dbo.Passengers", t => t.PassengerId, cascadeDelete: true)
                .Index(t => t.PassengerId);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        PassengerId = c.Int(nullable: false, identity: true),
                        PassengerName = c.String(),
                        BookingDate = c.DateTime(nullable: false),
                        MobileNo = c.String(),
                        IsTransit = c.Boolean(nullable: false),
                        FacilityId = c.Int(nullable: false),
                        BasicPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUrl = c.String(),
                        SeatType_SeatTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.PassengerId)
                .ForeignKey("dbo.SeatTypes", t => t.SeatType_SeatTypeId)
                .Index(t => t.SeatType_SeatTypeId);
            
            CreateTable(
                "dbo.SeatTypes",
                c => new
                    {
                        SeatTypeId = c.Int(nullable: false, identity: true),
                        SeatTypeName = c.String(),
                    })
                .PrimaryKey(t => t.SeatTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passengers", "SeatType_SeatTypeId", "dbo.SeatTypes");
            DropForeignKey("dbo.Facilities", "PassengerId", "dbo.Passengers");
            DropIndex("dbo.Passengers", new[] { "SeatType_SeatTypeId" });
            DropIndex("dbo.Facilities", new[] { "PassengerId" });
            DropTable("dbo.SeatTypes");
            DropTable("dbo.Passengers");
            DropTable("dbo.Facilities");
        }
    }
}
