namespace RoomReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newplaceinroomtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RRRooms", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RRRooms", "IsDeleted");
        }
    }
}
