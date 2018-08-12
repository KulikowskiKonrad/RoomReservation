namespace RoomReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class updatetablereservationandaddadnotation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms");
            DropForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers");
            DropIndex("dbo.RRReservations", new[] { "RRRoomId" });
            DropIndex("dbo.RRReservations", new[] { "RRUserId" });
            AddColumn("dbo.RRReservations", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.RRReservations", "RRRoomId", c => c.Long(nullable: false));
            AlterColumn("dbo.RRReservations", "RRUserId", c => c.Long(nullable: false));
            CreateIndex("dbo.RRReservations", "RRRoomId");
            CreateIndex("dbo.RRReservations", "RRUserId");
            AddForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.RRReservations", "Reservation");
        }

        public override void Down()
        {
            AddColumn("dbo.RRReservations", "Reservation", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers");
            DropForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms");
            DropIndex("dbo.RRReservations", new[] { "RRUserId" });
            DropIndex("dbo.RRReservations", new[] { "RRRoomId" });
            AlterColumn("dbo.RRReservations", "RRUserId", c => c.Long());
            AlterColumn("dbo.RRReservations", "RRRoomId", c => c.Long());
            DropColumn("dbo.RRReservations", "Date");
            CreateIndex("dbo.RRReservations", "RRUserId");
            CreateIndex("dbo.RRReservations", "RRRoomId");
            AddForeignKey("dbo.RRReservations", "RRUserId", "dbo.RRUsers", "Id");
            AddForeignKey("dbo.RRReservations", "RRRoomId", "dbo.RRRooms", "Id");
        }
    }
}
