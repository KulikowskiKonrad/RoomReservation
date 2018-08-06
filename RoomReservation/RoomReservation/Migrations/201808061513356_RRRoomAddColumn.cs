namespace RoomReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RRRoomAddColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RRRooms", "OrDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RRRooms", "OrDeleted");
        }
    }
}
