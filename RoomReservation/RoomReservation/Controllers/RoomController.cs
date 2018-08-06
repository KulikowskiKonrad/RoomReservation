using RoomReservation.DB.Models;
using RoomReservation.Helpers;
using RoomReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomReservation.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult RoomList()
        {
            try
            {
                RoomRepository roomRepository = new RoomRepository();
                List<RRRoom> room = roomRepository.PobierzWszystkie();
                return View("RoomList", room);

            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }
    }
}