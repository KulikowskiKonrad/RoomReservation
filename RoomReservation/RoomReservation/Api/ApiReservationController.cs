using RoomReservation.DB.Models;
using RoomReservation.Helpers;
using RoomReservation.Models;
using RoomReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace RoomReservation.Api
{
    public class ApiReservationController : ApiController
    {

        private ReservationRepository _reservationRepository = new ReservationRepository();
        protected long UserId
        {
            get
            {
                return long.Parse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            }
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<ReservationListItem> result = _reservationRepository.GetAll(UserId).Select(x => new ReservationListItem()
            {
                Date = x.Date,
                Id = x.Id,
                StatusText = x.Status.ToString(),
                RoomName = x.Room.Name,
                UserEmail = x.User.Email,
                RoomId = x.RRRoomId
            }).ToList();
            return Ok(result);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri]long id)
        {
            RRReservation reservationToDelete = _reservationRepository.GetById(id);
            reservationToDelete.IsDeleted = true;
            long? saveResult = _reservationRepository.Save(reservationToDelete);
            if (saveResult.HasValue)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult SaveReservationDetails(EditReservationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ReservationRepository reservationRepository = new ReservationRepository();
                    RRReservation reservation = null;
                    if (model.Id.HasValue)
                    {
                        reservation = reservationRepository.GetById(model.Id.Value);
                    }
                    else
                    {
                        reservation = new RRReservation();
                        reservation.Status = RoomReservation.Enum.ReservationStatus.InProgress;
                    }
                    reservation.Date = model.Date.Value;
                    reservation.RRRoomId = model.RRRoomId.Value;
                    reservation.RRUserId = UserId;
                    long? rezultatZapisu = reservationRepository.Save(reservation);
                    if (rezultatZapisu == null)
                    {
                        return InternalServerError();
                    }
                    else
                    {
                        return Ok();
                    }
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }

    }
}
