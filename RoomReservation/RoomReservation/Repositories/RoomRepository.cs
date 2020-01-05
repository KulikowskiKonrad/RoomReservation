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

        private int _pole1;
        public int Pole1
        {
            get
            {
                if (_pole1 == -1)
                {
                    //pobierz wartosc z bazy i utaw ja Pole1 = wartosczbazy
                }
                return _pole1;
            }
            set
            {
                _pole1 = value;
            }
        }


        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string PelneImieINaziwsko
        {
            get
            {
                return Imie + " " + Nazwisko;
            }
        }

        public List<RRRoom> DownloadAll()
        {
            try
            {
                List<RRRoom> roomList = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    string mojaZmienna = string.Format("tekst {0}  {1}", DateTime.Now, DateTime.Today);
                    string mojaZmienna1 = "tekst " + DateTime.Now + "     " + DateTime.Today;
                    string mojaZmienna2 = $"tekt {DateTime.Now}    {DateTime.Today}";

                    roomList = db.Rooms.Where(x => x.IsDeleted == false).ToList();
                    return roomList;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public RRRoom Download(long roomId)
        {
            try
            {
                RRRoom result = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    result = db.Rooms.Where(x => x.Id == roomId).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Save(RRRoom room)
        {
            try
            {
                long? result = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    db.Entry(room).State = room.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.SaveChanges();
                    result = room.Id;
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
            try
            {
                bool result = false;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    RRRoom rRRoom = null;
                    rRRoom = db.Rooms.Where(x => x.Id == id).Single();
                    rRRoom.IsDeleted = true;
                    db.SaveChanges();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return false;
            }
        }

    }
}