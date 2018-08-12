namespace RoomReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RRReservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RRReservations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RRRoomId = c.Long(nullable: false),
                        RRUserId = c.Long(nullable: false),
                        BookingFrom = c.DateTime(nullable: false),
                        BookingTo = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RRRooms", t => t.RRRoomId, cascadeDelete: true)
                .ForeignKey("dbo.RRUsers", t => t.RRUserId, cascadeDelete: true)
                .Index(t => t.RRRoomId)
                .Index(t => t.RRUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers");
            DropForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms");
            DropIndex("dbo.RRReservations", new[] { "RRUserId" });
            DropIndex("dbo.RRReservations", new[] { "RRRoomId" });
            DropTable("dbo.RRReservations");
        }
    }
}
