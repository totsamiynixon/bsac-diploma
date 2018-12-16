using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Data.Implementations.Context;
using Duke.Owin.VkontakteMiddleware;
using Entity.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
using Owin;
using WebUI.Identity;

namespace WebUI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        //public static ApplicationOAuthProvider OAuthProvider => OAuthOptions.Provider as ApplicationOAuthProvider;

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new DataContext());
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());
            PublicClientId = "BSACDiploma";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }

    //public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    //{


    //    private readonly string _publicClientId;

    //    public ApplicationOAuthProvider(string publicClientId)
    //    {
    //        if (publicClientId == null)
    //        {
    //            throw new ArgumentNullException("publicClientId");
    //        }

    //        _publicClientId = publicClientId;
    //    }

    //    public async Task<AuthenticationTicket> GenerateTicketAsync(User user, ApplicationUserManager userManager)
    //    {
    //        ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
    //        var roles = userManager.GetRoles(user.Id);
    //        AuthenticationProperties properties = CreateProperties(user.UserName, roles);
    //        return new AuthenticationTicket(oAuthIdentity, properties);
    //    }

    //    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    //    {
    //        var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

    //        User user = await userManager.FindAsync(context.UserName, context.Password);

    //        if (user == null)
    //        {
    //            context.SetError("invalid_grant", "The user name or password is incorrect.");
    //            return;
    //        }
    //        var ticket = await GenerateTicketAsync(user, userManager);
    //        context.Validated(ticket);
    //    }

    //    public override Task TokenEndpoint(OAuthTokenEndpointContext context)
    //    {
    //        foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
    //        {
    //            context.AdditionalResponseParameters.Add(property.Key, property.Value);
    //        }
    //        return Task.FromResult<object>(null);
    //    }

    //    public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    //    {

    //        if (context.Request.Method == "OPTIONS")
    //        {
    //            context.Response.Headers.Add("Access-Control-Allow-Origin", new string[] { "*" });
    //            context.Response.StatusCode = 200;
    //        }
            
    //        // Resource owner password credentials does not provide a client ID.
    //        if (context.ClientId == null)
    //        {
    //            context.Validated();
    //        }

    //        return Task.FromResult<object>(null);
    //    }

    //    public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
    //    {
    //        if (context.ClientId == _publicClientId)
    //        {
    //            Uri expectedRootUri = new Uri(context.Request.Uri, "/");

    //            if (expectedRootUri.AbsoluteUri == context.RedirectUri)
    //            {
    //                context.Validated();
    //            }
    //        }

    //        return Task.FromResult<object>(null);
    //    }

    //    public static AuthenticationProperties CreateProperties(string userName, IEnumerable<string> roles)
    //    {
    //        IDictionary<string, string> data = new Dictionary<string, string>
    //        {
    //            { "userName", userName },
    //            { "roles", string.Join(" ,", roles) }
    //        };
    //        return new AuthenticationProperties(data);
    //    }
    //}
}