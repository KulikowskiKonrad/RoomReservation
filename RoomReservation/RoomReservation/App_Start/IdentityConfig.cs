//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;
//using Microsoft.Owin.Security;
//using RoomReservation.Models;

//namespace RoomReservation
//{
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using RoomReservation.DB.Models;
using RoomReservation.Enum;
using RoomReservation.Models;
using RoomReservation.Repositories;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.SessionState;

public class ApplicationSignInManager
{
    private HttpSessionState Session
    {
        get
        {
            return HttpContext.Current.Session;
        }
    }
    private IOwinContext OwinContext { get; set; }
    public ApplicationSignInManager(IOwinContext owinContext)
    {
        OwinContext = owinContext;
    }

    public SignInStatus PasswordSignIn(LoginViewModel model)
    {
        SignInStatus result = SignInStatus.Failure;

        RRUser user = new UserRepository().Pobierz(model.Email, model.Password);
        if (user != null)
        {
            result = SignInStatus.Success;
            var ident = new ClaimsIdentity(
              new[] {
                      new Claim(ClaimTypes.NameIdentifier, model.Email),
                      new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                      new Claim(ClaimTypes.Name, model.Email),
                      new Claim(ClaimTypes.Role, ((UserRole)user.Role).ToString()),
                      new Claim("UserId", user.Id.ToString())
              },
              DefaultAuthenticationTypes.ApplicationCookie);

            OwinContext.Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = model.RememberMe ? (DateTime?)DateTime.UtcNow.AddDays(7) : null // jezeli wybrano zapamietaj mnie to uzytkownik bedzie zalogowany przez 7 dni
            }, ident);
            result = SignInStatus.Success;
            Session["uzytkownik"] = user;

            //SessionHelper.LoggedUser = user;
            //SessionHelper.UserId = user.Id;
        }

        return result;
    }
}
//    public class EmailService : IIdentityMessageService
//    {
//        public Task SendAsync(IdentityMessage message)
//        {
//            // Plug in your email service here to send an email.
//            return Task.FromResult(0);
//        }
//    }

//    public class SmsService : IIdentityMessageService
//    {
//        public Task SendAsync(IdentityMessage message)
//        {
//            // Plug in your SMS service here to send a text message.
//            return Task.FromResult(0);
//        }
//    }

//    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
//    public class ApplicationUserManager : UserManager<ApplicationUser>
//    {
//        public ApplicationUserManager(IUserStore<ApplicationUser> store)
//            : base(store)
//        {
//        }

//        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
//        {
//            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
//            // Configure validation logic for usernames
//            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
//            {
//                AllowOnlyAlphanumericUserNames = false,
//                RequireUniqueEmail = true
//            };

//            // Configure validation logic for passwords
//            manager.PasswordValidator = new PasswordValidator
//            {
//                RequiredLength = 6,
//                RequireNonLetterOrDigit = true,
//                RequireDigit = true,
//                RequireLowercase = true,
//                RequireUppercase = true,
//            };

//            // Configure user lockout defaults
//            manager.UserLockoutEnabledByDefault = true;
//            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
//            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

//            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
//            // You can write your own provider and plug it in here.
//            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
//            {
//                MessageFormat = "Your security code is {0}"
//            });
//            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
//            {
//                Subject = "Security Code",
//                BodyFormat = "Your security code is {0}"
//            });
//            manager.EmailService = new EmailService();
//            manager.SmsService = new SmsService();
//            var dataProtectionProvider = options.DataProtectionProvider;
//            if (dataProtectionProvider != null)
//            {
//                manager.UserTokenProvider = 
//                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
//            }
//            return manager;
//        }
//    }

//    // Configure the application sign-in manager which is used in this application.
//    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
//    {
//        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
//            : base(userManager, authenticationManager)
//        {
//        }

//        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
//        {
//            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
//        }

//        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
//        {
//            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
//        }
//    }
//}
