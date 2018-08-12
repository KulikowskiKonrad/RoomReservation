using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomReservation.Models
{
    public class ChangeStatus
    {

        public long Id { get; set; }

        public RoomReservation.Enum.ReservationStatus Status { get; set; }

    }
}