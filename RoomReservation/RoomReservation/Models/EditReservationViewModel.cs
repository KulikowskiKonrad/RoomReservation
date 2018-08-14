using RoomReservation.DB.Models;
using RoomReservation.Enum;
using RoomReservation.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomReservation.Models
{
    public class EditReservationViewModel
    {

        public long? Id { get; set; }

        [Display(Name = "Pomieszczenie")]
        [Required(ErrorMessage = "Pole wymagane")]
        public long? RRRoomId { get; set; }

        //public long RRUserId { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Data rezerwacji")]
        public DateTime? Date { get; set; }

    }
}