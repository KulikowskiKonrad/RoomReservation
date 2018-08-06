namespace RoomReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RRRoom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RRRooms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Localization = c.String(maxLength: 80),
                        NameRoom = c.String(),
                        OrBooked = c.Boolean(nullable: false),
                        UserId = c.Long(nullable: false),
                        BookingFrom = c.DateTime(nullable: false),
                        BookingTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RRRooms");
        }
    }
}
