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
                List<RRReservation> listaRezerwacji = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    listaRezerwacji = baza.Reservation.Include(x => x.Room).Include(y => y.User)
                        .Where(x => x.IsDeleted == false && (x.RRUserId == userId || userId == null)
                            && (!roomId.HasValue || x.RRRoomId == roomId))
                        .ToList();
                    return listaRezerwacji;
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
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    reservation = baza.Reservation.Where(x => x.RRRoomId == roomId && x.Date == date && x.IsDeleted == false).SingleOrDefault();
                    return reservation;
                }
            }
            catch (Exception ex)
            {

                LogHelper.Log.Error(ex);
                return null;
            }
        }
        public RRReservation GetById(long rezerwacjaId)
        {
            try
            {
                RRReservation rezultat = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    rezultat = baza.Reservation.Where(x => x.Id == rezerwacjaId).SingleOrDefault();
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Save(RRReservation rezerwacja)
        {
            try
            {
                long? rezultat = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    baza.Entry(rezerwacja).State = rezerwacja.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = rezerwacja.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public bool Delete(long id)
        {
            bool rezultat = false;
            try
            {
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    RRReservation reservation = null;
                    reservation = baza.Reservation.Where(x => x.Id == id).Single();
                    reservation.IsDeleted = true;
                    baza.SaveChanges();
                    rezultat = true;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}