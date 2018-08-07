namespace RoomReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RRRoomChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RRRooms", "Details", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.RRRooms", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.RRRooms", "Localization");
            DropColumn("dbo.RRRooms", "NameRoom");
            DropColumn("dbo.RRRooms", "OrBooked");
            DropColumn("dbo.RRRooms", "UserId");
            DropColumn("dbo.RRRooms", "BookingFrom");
            DropColumn("dbo.RRRooms", "BookingTo");
            DropColumn("dbo.RRRooms", "OrDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RRRooms", "OrDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.RRRooms", "BookingTo", c => c.DateTime(nullable: false));
            AddColumn("dbo.RRRooms", "BookingFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.RRRooms", "UserId", c => c.Long(nullable: false));
            AddColumn("dbo.RRRooms", "OrBooked", c => c.Boolean(nullable: false));
            AddColumn("dbo.RRRooms", "NameRoom", c => c.String());
            AddColumn("dbo.RRRooms", "Localization", c => c.String(maxLength: 80));
            DropColumn("dbo.RRRooms", "Name");
            DropColumn("dbo.RRRooms", "Details");
        }
    }
}
