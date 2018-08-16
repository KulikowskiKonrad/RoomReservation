using RoomReservation.DB;
using RoomReservation.DB.Models;
using RoomReservation.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RoomReservation.Repositories
{
    public class ReservationRepository
    {

        public List<RRReservation> GetAll(long? userId, long? roomId)
        {
            try
            {
                List<RRReservation> reservationList = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    reservationList = db.Reservation.Include(x => x.Room).Include(y => y.User)
                        .Where(x => x.IsDeleted == false && (x.RRUserId == userId || userId == null)
                            && (!roomId.HasValue || x.RRRoomId == roomId))
                        .ToList();
                    return reservationList;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }
        public RRReservation GetByIdDate(long? roomId, DateTime date)
        {
            try
            {
                RRReservation reservation = new RRReservation();
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    reservation = db.Reservation.Where(x => x.RRRoomId == roomId && x.Date == date && x.IsDeleted == false).SingleOrDefault();
                    return reservation;
                }
            }
            catch (Exception ex)
            {

                LogHelper.Log.Error(ex);
                return null;
            }
        }
        public RRReservation GetById(long reservationId)
        {
            try
            {
                RRReservation result = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    result = db.Reservation.Where(x => x.Id == reservationId).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Save(RRReservation reservation)
        {
            try
            {
                long? result = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    db.Entry(reservation).State = reservation.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.SaveChanges();
                    result = reservation.Id;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public bool Delete(long id)
        {
            bool result = false;
            try
            {
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    RRReservation reservation = null;
                    reservation = db.Reservation.Where(x => x.Id == id).Single();
                    reservation.IsDeleted = true;
                    db.SaveChanges();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}