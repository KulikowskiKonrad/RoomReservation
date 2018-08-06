using RoomReservation.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomReservation.DB.Models
{
    public class RRReservation
    {
        public long Id { get; set; }

        public long RRRoomId { get; set; }

        public long RRUserId { get; set; }


        public DateTime BookingFrom { get; set; }

        public DateTime BookingTo { get; set; }

        public ReservationStatus Status { get; set; }

        public virtual RRRoom Room { get; set; }

        public virtual RRUser User { get; set; }

    }
}