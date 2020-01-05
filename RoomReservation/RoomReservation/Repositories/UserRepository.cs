using RoomReservation.DB;
using RoomReservation.DB.Models;
using RoomReservation.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web;

namespace RoomReservation.Repositories
{
    public class UserRepository
    {

        public RRUser GetByEmailPassword(string email, string password)
        {
            try
            {
                RRUser result = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    result = db.Users.Where(x => x.Email == email).SingleOrDefault();

                    //using (TransactionScope tran = new TransactionScope())
                    //{
                    //    RRRoom room1 = new RRRoom()
                    //    {
                    //        Details = "test 1",
                    //        Name = "t1"
                    //    };
                    //    db.Rooms.Add(room1);
                    //    db.SaveChanges();

                    //    room1 = new RRRoom()
                    //    {
                    //        Name = "t2"
                    //    };
                    //    db.Rooms.Add(room1);
                    //    db.SaveChanges();

                    //if(asdasdsad)
                    //    tran.Complete();
                    //}
                }
                if (result != null)
                {
                    string encodedPassword = MD5Helper.GenerateMD5(password + result.Salt);
                    if (encodedPassword != result.Password)
                    {
                        result = null;
                    }
                }




                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public RRUser GetByLogin(string email)
        {
            try
            {
                RRUser result = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    result = db.Users.Where(x => x.Email == email).SingleOrDefault();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public RRUser GetById(long userId)
        {
            try
            {
                RRUser result = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    result = db.Users.Where(x => x.Id == userId).SingleOrDefault();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Save(RRUser rUser)
        {
            try
            {
                long? result = null;
                using (RoomReservationContext db = new RoomReservationContext())
                {
                    db.Entry(rUser).State = rUser.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.SaveChanges();
                    result = rUser.Id;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }
    }
}