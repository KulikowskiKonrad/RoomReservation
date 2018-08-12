using RoomReservation.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RoomReservation.DB
{
    public class RoomReservationContext : DbContext
    {

        public DbSet<RRUser> Users { get; set; }
        public DbSet<RRRoom> Rooms { get; set; }
        public DbSet<RRReservation> Reservation { get; set; }
        public RoomReservationContext()
            : base("DefaultConnection")
        {
        }
    }
}