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
    public class RoomRepository
    {

        public List<RRRoom> PobierzWszystkie()
        {
            try
            {
                List<RRRoom> listaPomieszczen = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    listaPomieszczen = baza.Rooms.ToList();
                    return listaPomieszczen;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public RRRoom Pobierz(long pomieszczenieId)
        {
            try
            {
                RRRoom rezultat = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    rezultat = baza.Rooms.Where(x => x.Id == pomieszczenieId).SingleOrDefault();
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Zapisz(RRRoom pomieszczenie)
        {
            try
            {
                long? rezultat = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    baza.Entry(pomieszczenie).State = pomieszczenie.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = pomieszczenie.Id;
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public bool Usun(long pomieszczenieId)
        {
            try
            {
                bool rezultat = false;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    RRRoom rRRoom = null;
                    rRRoom = baza.Rooms.Where(x => x.Id == pomieszczenieId).Single();
                    rRRoom.OrDeleted = true;
                    baza.SaveChanges();
                    rezultat = true;
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return false;
            }
        }

    }
}