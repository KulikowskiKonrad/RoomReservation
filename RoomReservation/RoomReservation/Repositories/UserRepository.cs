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
    public class UserRepository
    {

        public RRUser Pobierz(string email, string haslo)
        {
            try
            {
                RRUser rezultat = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    rezultat = baza.Users.Where(x => x.Email == email).SingleOrDefault();
                }
                if (rezultat != null)
                {
                    string hasloZakodowane = MD5Helper.GenerateMD5(haslo + rezultat.Salt);
                    if (hasloZakodowane != rezultat.Password)
                    {
                        rezultat = null;
                    }
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public RRUser Pobierz(string email)
        {
            try
            {
                RRUser rezultat = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    rezultat = baza.Users.Where(x => x.Email == email).SingleOrDefault();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public RRUser Pobierz(long uzytkownikId)
        {
            try
            {
                RRUser rezultat = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    rezultat = baza.Users.Where(x => x.Id == uzytkownikId).SingleOrDefault();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Zapisz(RRUser rUser)
        {
            try
            {
                long? rezultat = null;
                using (RoomReservationContext baza = new RoomReservationContext())
                {
                    baza.Entry(rUser).State = rUser.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = rUser.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }
    }
}