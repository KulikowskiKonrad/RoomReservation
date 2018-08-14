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
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult RoomList()
        {
            try
            {
                //RoomRepository roomRepository = new RoomRepository();
                //List<RRRoom> room = roomRepository.DownloadAll();
                return View("RoomList");

            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RoomDetails(long? id)
        {
            try
            {
                EditRoomViewModel model = new EditRoomViewModel();
                model.Id = id;
                RoomRepository roomRepository = new RoomRepository();
                if (id.HasValue)
                {
                    RRRoom pobranePomieszczenie = roomRepository.Download(id.Value);
                    model.Id = pobranePomieszczenie.Id;
                    model.Name = pobranePomieszczenie.Name;
                    model.Details = pobranePomieszczenie.Details;
                }
                return View("RoomDetails", model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveRoomDetails(EditRoomViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RoomRepository roomRepository = new RoomRepository();
                    RRRoom room = null;
                    if (model.Id.HasValue)
                    {
                        room = roomRepository.Download(model.Id.Value);
                    }
                    else
                    {
                        room = new RRRoom();
                    }
                    room.Name = model.Name;
                    room.Details = model.Details;
                    long? rezultatZapisu = roomRepository.Save(room);
                    if (rezultatZapisu == null)
                    {
                        return View("Error");
                    }
                    else
                    {
                        return RedirectToAction("RoomList");
                    }
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    RoomRepository roomRepository = new RoomRepository();
                    bool rezultatUsuniecia = roomRepository.Delete(id);
                    return RedirectToAction("RoomList");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

    }
}