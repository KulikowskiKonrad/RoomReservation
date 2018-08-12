namespace RoomReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolumninreservationtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RRReservations", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RRReservations", "IsDeleted");
        }
    }
}
