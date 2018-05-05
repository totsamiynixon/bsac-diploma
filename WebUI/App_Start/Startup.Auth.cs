using System;
using System.Web;
using System.Web.Http;
using Duke.Owin.VkontakteMiddleware;
using Entity.Domain.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Owin;
using WebUI.Identity;

namespace WebUI
{
    public partial class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request

            DataProtectionProvider = app.GetDataProtectionProvider();
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
                    OnValidateIdentity = SecurityStampValidator
                        .OnValidateIdentity<UserManager<User, int>, User, int>(
                            validateInterval: TimeSpan.FromMinutes(30),
                            regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                            getUserIdCallback: (user) => int.Parse(user.GetUserId())),
                    OnApplyRedirect = ctx =>
                    {
                        if (!HttpContext.Current.Request.Url.AbsoluteUri.Contains("/api/"))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                },
                CookieName = "ApplicationAuth"
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            app.UseTwitterAuthentication(new TwitterAuthenticationOptions()
                {
                    ConsumerKey = "NMHW0d13DaqAaywHf7f2VvJYt",
                    ConsumerSecret = "RQaRTSp8QIijhWiag8tBD1mCXruJ1OulbQPR2jOgPjMlwpJh6s",
                    BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new[]
                    {
                        "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                        "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                        "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                        "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                        "5168FF90AF0207753CCCD9656462A212B859723B", //DigiCert SHA2 High Assurance Server C‎A 
                        "B13EC36903F8BF4701D498261A0802EF63642BC3" //DigiCert High Assurance EV Root CA
                    })
                }
            );


            //app.UseFacebookAuthentication(
            //   appId: "164530750755602",
            //   appSecret: "80f3997e319283694ef6b09c75961a88");

            app.UseFacebookAuthentication(
                appId: "675554055963544",
                appSecret: "1bf98658d01aef192d54faa20c6adf2e");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "840725586783-jd43671i0fs4j5qkdbmn3p6r6k7p9t3l.apps.googleusercontent.com",
                ClientSecret = "PrgjP8UxBU2WCZvljDPdm3m6"
            });
            app.UseVkontakteAuthentication(new VkAuthenticationOptions()
            {
                AppId = "6104507",
                AppSecret = "kklrlzvdTN2z6VgXdV3M",


            });
        }
    }
}