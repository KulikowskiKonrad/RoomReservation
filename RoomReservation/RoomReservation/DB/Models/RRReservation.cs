using RoomReservation.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservation.DB.Models
{
    public class RRReservation
    {
        public long Id { get; set; }

        public long RRRoomId { get; set; }

        public long RRUserId { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Rezerwacja od")]
        public DateTime BookingFrom { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Rezerwacja do")]
        public DateTime BookingTo { get; set; }

        public ReservationStatus Status { get; set; }

        public virtual RRRoom Room { get; set; }

        public virtual RRUser User { get; set; }

    }
}