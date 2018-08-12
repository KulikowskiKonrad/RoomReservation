using RoomReservation.Helpers;
using RoomReservation.Models;
using RoomReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RoomReservation.DB.Models;
using RoomReservation.Enum;
using System.Net;

namespace RoomReservation.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edytuj()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }



        //[HttpPost]
        //[Authorize(Roles = "Admion")]
        //public ActionResult Edytuj(EdytujUzytkownikaViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid == true)
        //        {
        //            UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
        //            Uzytkownik uzytkownik = uzytkownikRepozytorium.Pobierz(((Uzytkownik)Session["uzytkownik"]).Id);
        //            string sol = Guid.NewGuid().ToString();
        //            uzytkownik.Sol = sol;
        //            uzytkownik.Haslo = MD5Helper.GenerujMD5(model.Haslo + sol);
        //            long? rezultatEdycji = uzytkownikRepozytorium.Zapisz(uzytkownik);
        //            if (rezultatEdycji != null)
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                return View("Error");
        //            }
        //        }
        //        else
        //        {
        //            return View("Edytuj", model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log.Error(ex);
        //        return View("Error");
        //    }
        //}

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                UserRepository uzytkownikRepozytorium = new UserRepository();
                return View("Login");
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)

        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var result = new ApplicationSignInManager(HttpContext.GetOwinContext()).PasswordSignIn(model);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            return RedirectToAction("Index", "Home");

                        case SignInStatus.LockedOut:
                        case SignInStatus.RequiresVerification:
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("Haslo", "Niepoprawny login lub hasło");
                            return View("Login", model);
                    }

                }
                else
                {
                    return View("Login", model);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        //[HttpPost]
        //public ActionResult Register(RegisterViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid == true)
        //        {
        //            UserRepository uzytkownikRepozytorium = new UserRepository();
        //            RRUser pobranyUzytkownik = uzytkownikRepozytorium.Pobierz(model.Email);
        //            if (pobranyUzytkownik == null)
        //            {
        //                string sol = Guid.NewGuid().ToString(); //robie sol jako GUID i zamieniam na string

        //                RRUser uzytkownik = new RRUser()
        //                {
        //                    Salt = sol,
        //                    Email = model.Email,
        //                    Password = MD5Helper.GenerateMD5(model.Password + sol), //generujemy md5 z polaczenia hasla i soli (losowego ciagu znakow) wywoluje metode statyczna z klasy
        //                                                                            //MD5Helper
        //                    Role = UserRole.Standard
        //                };
        //                long? rezultatZapisu = uzytkownikRepozytorium.Zapisz(uzytkownik);
        //                if (rezultatZapisu != null)
        //                {
        //                    return RedirectToAction("Login", "Account");
        //                }
        //                else
        //                {
        //                    return View("Error");
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("Login", "Login jest już zajęty");
        //                return View("Register", model);
        //            }
        //        }
        //        else
        //        {
        //            return View("Register", model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log.Error(ex);
        //        return View("Error");
        //    }

        //}

        [HttpPost]
        public ActionResult LogOff()
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    if (Request.IsAuthenticated == true)
                    {
                        Session.Abandon();
                        HttpContext.GetOwinContext().Authentication.SignOut();
                    }
                    return RedirectToAction("Login");
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
