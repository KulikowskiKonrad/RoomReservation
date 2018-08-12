using RoomReservation.DB.Models;
using RoomReservation.Enum;
using RoomReservation.Helpers;
using RoomReservation.Models;
using RoomReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoomReservation.Api
{
    public class ApiUserController : ApiBaseController
    {
        private UserRepository _userRepository = new UserRepository();

        [HttpPost]
        public IHttpActionResult RegisterUser([FromBody] RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RRUser pobranyUzytkownik = _userRepository.Pobierz(model.Email);
                    if (pobranyUzytkownik == null)
                    {
                        string salt = Guid.NewGuid().ToString(); //robie sol jako GUID i zamieniam na string

                        RRUser uzytkownik = new RRUser()
                        {
                            Salt = salt,
                            Email = model.Email,
                            Password = MD5Helper.GenerateMD5(model.Password + salt), //generujemy md5 z polaczenia hasla i soli (losowego ciagu znakow) wywoluje metode statyczna z klasy
                                                                                     //MD5Helper
                            Role = UserRole.Standard
                        };
                        long? registeredUserId = _userRepository.Zapisz(uzytkownik);
                        if (registeredUserId != null)
                        {
                            return Ok(registeredUserId);
                        }
                        else
                        {
                            return Content(HttpStatusCode.BadRequest, "Błąd zapisu użytkownika");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, "Login jest już zajęty");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Błędne dane");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
                //return Content(HttpStatusCode.BadRequest, "Any object");
            }
        }
    }
}
