using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservation.Models
{
    public class RoomListItem
    {
        public long Id { get; set; }

        [StringLength(200)]
        [Required]
        public string Details { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}