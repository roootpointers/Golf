using System; 
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Project.Models;
using System.Web.Security;
using Project.Models.Database;

namespace Project.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Error = TempData["Error"];
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginCheck(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please Enter the Records Carefully";
                return RedirectToAction("Login", "Account");
            }
            try
            {
                Admins user = Models.DataClasses.SecurityClass.GetLoginStatus(model);

                if (user != null)
                {
                    string roles = "Admins,Scoreboard";
                    DateTime cookieIssuedDate = DateTime.Now;

                    var ticket = new FormsAuthenticationTicket(0,
                        user.ID.ToString(),
                        cookieIssuedDate,
                        DateTime.Now.AddMinutes(2880),
                        false,
                        roles);

                    string encryptedCookieContent = FormsAuthentication.Encrypt(ticket);
                    var formsAuthenticationTicketCookie = new HttpCookie("AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd", encryptedCookieContent)
                    {
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath,
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL
                    };

                    System.Web.HttpContext.Current.Response.Cookies.Add(formsAuthenticationTicketCookie);
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Scoreboard", "Admin");
                }
                else
                {
                    TempData["Error"] = "Username or password is Incorrect!";
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Username or password is Incorrect!";
                return RedirectToAction("Login", "Account");
            }
        }  
     
        // POST: /Account/LogOff
        [AllowAnonymous] 
        public ActionResult LogOff()
        {
            HttpCookie oldCookie = new HttpCookie("AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd");
            oldCookie.Expires = DateTime.Now.AddDays(-3);
            Response.Cookies.Add(oldCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Scoreboard", "Admin");
        }
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}