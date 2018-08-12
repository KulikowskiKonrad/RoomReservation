namespace RoomReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatereservationtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms");
            DropForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers");
            DropIndex("dbo.RRReservations", new[] { "RRRoomId" });
            DropIndex("dbo.RRReservations", new[] { "RRUserId" });
            AddColumn("dbo.RRReservations", "Reservation", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RRReservations", "RRRoomId", c => c.Long());
            AlterColumn("dbo.RRReservations", "RRUserId", c => c.Long());
            CreateIndex("dbo.RRReservations", "RRRoomId");
            CreateIndex("dbo.RRReservations", "RRUserId");
            AddForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms", "Id");
            AddForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers", "Id");
            DropColumn("dbo.RRReservations", "BookingFrom");
            DropColumn("dbo.RRReservations", "BookingTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RRReservations", "BookingTo", c => c.DateTime(nullable: false));
            AddColumn("dbo.RRReservations", "BookingFrom", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers");
            DropForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms");
            DropIndex("dbo.RRReservations", new[] { "RRUserId" });
            DropIndex("dbo.RRReservations", new[] { "RRRoomId" });
            AlterColumn("dbo.RRReservations", "RRUserId", c => c.Long(nullable: false));
            AlterColumn("dbo.RRReservations", "RRRoomId", c => c.Long(nullable: false));
            DropColumn("dbo.RRReservations", "Reservation");
            CreateIndex("dbo.RRReservations", "RRUserId");
            CreateIndex("dbo.RRReservations", "RRRoomId");
            AddForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms", "Id", cascadeDelete: true);
        }
    }
}
