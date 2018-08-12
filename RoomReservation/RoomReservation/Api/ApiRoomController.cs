using RoomReservation.Models;
using RoomReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoomReservation.Api
{
    public class ApiRoomController : ApiController
    {
        public List<RoomListItem> GetAll()
        {
            List<RoomListItem> result = new RoomRepository().DownloadAll().Select(x => new RoomListItem()
            {
                Details = x.Details,
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return result;
        }
    }
}
