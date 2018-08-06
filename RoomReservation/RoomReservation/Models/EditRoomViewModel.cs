using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservation.Models
{
    public class EditRoomViewModel
    {

        public long? Id { get; set; }

        [StringLength(80)]
        [Display(Name = "Lokalizacja")]
        public string Localization { get; set; }

        [StringLength(80, MinimumLength = 4)]
        public string NameRoom { get; set; }

        public bool OrBooked { get; set; }

        [Display(Name = "Użytkownik")]
        public long UserId { get; set; }

        [Display(Name = "Rezerwacja od")]
        public DateTime BookingFrom { get; set; }

        [Display(Name = "Rezerwacja do")]
        public DateTime BookingTo { get; set; }


        public bool OrDeleted { get; set; }

    }
}