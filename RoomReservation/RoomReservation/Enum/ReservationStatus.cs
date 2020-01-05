using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoomReservation.Enum
{
    public enum ReservationStatus
    {
        [Description("W toku")]
        InProgress = 1,

        [Description("Zaakceptowany")]
        Accepted = 2,
        //
        [Description("Odrzucony")]
        Rejected = 3,
    }
}