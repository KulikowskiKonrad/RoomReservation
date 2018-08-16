using RoomReservation.DB.Models;
using RoomReservation.Helpers;
using RoomReservation.Models;
using RoomReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomReservation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoomController : Controller
    {
        [HttpGet]
        public ActionResult RoomList()
        {
            try
            {
                return View("RoomList");
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }
    }
}