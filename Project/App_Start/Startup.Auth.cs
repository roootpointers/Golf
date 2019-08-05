﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies; 
using Owin;
using Project.Models; 

namespace Project
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(2880),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(2880));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //var FacebookAuthenticationOptions = new FacebookAuthenticationOptions()
            //{
            //    AppId = ConfigurationManager.AppSettings["FacebookAppId"],
            //    AppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"],
            //    Scope = { "email" },
            //    UserInformationEndpoint = "https://graph.facebook.com/v2.7/me?fields=id,name,email",
            //    Provider = new FacebookAuthenticationProvider()
            //    {
            //        OnAuthenticated = Context =>
            //        {
            //            Context.Identity.AddClaim(new System.Security.Claims.Claim("urn:tokens:facebook", Context.AccessToken));
            //            return Task.FromResult(0);
            //        }
            //    },
            //    SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
            //    SendAppSecretProof = true,
            //    //BackchannelHttpHandler = new FacebookBackChannelHandler(),    
            //};
            //FacebookAuthenticationOptions.Scope.Add("email");
            //app.UseFacebookAuthentication(FacebookAuthenticationOptions);

            //app.UseFacebookAuthentication(
            //  appId: "2014951002083536",
            //  appSecret: "66e2167999ddaddeaddbd458d6d64a4f");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}