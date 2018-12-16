using Entity.Domain.Identity;
using Entity.Domain.Settings;
using Entity.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Services.Features.DTO.Settings;
using Services.Features.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using WebUI.Identity;
using WebUI.Models.Identity;

namespace WebUI.Controllers.API
{
    [RoutePrefix("api/account")]
    [Authorize]
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ISettingsService _settingsService;
        public AccountController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager,
            ISettingsService settingsService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _settingsService = settingsService;
        }

        // POST: /Account/Login
        [HttpPost]
        [Route("sign-in")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = await _userManager.FindAsync(model.Email, model.Password);
            if (user == null)
            {
                return BadRequest("No such user!");
            }
            var token = await GenerateTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user.Id);
            var result = new
            {
                Id = user.Id,
                Name = user.UserName,
                Roles = await _userManager.GetRolesAsync(user.Id),
                Token = token.token,
                Expires = token.expiresUtc,
                Issued = token.issuedUtc
            };
            return Ok(result);
        }
        // POST: /Account/Register
        [HttpPost]
        [Route("sign-up")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { UserName = model.Email, Email = model.Email, Settings = new Settings() };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var userInDb = await _userManager.FindByEmailAsync(user.Email);
                    result = await _userManager.AddToRoleAsync(userInDb.Id, Roles.User);
                    if (result.Succeeded)
                    {
                        var token = await GenerateTokenAsync(userInDb);
                        return Ok(new
                        {
                            Id = user.Id,
                            Name = user.UserName,
                            Expires = token.expiresUtc,
                            Issued = token.issuedUtc,
                            Roles = await _userManager.GetRolesAsync(user.Id),
                            Token = token.token
                        });
                    }
                }
            }
            return BadRequest("Error");
        }

        // POST: /Account/Register
        [HttpGet]
        [Route("fullinfo")]
        public async Task<IHttpActionResult> GetFullInfoAsync()
        {
            var userId = User.Identity.GetUserId<int>();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Error");
            }
            var settings = await _settingsService.GetSettingsAsync(userId);
            return Ok(new
            {
                id = user.Id,
                userName = user.UserName,
                roles = await GetRolesAsStringAsync(user),
                settings = settings
            });
        }
        private async Task<(string token, DateTime expiresUtc, DateTime issuedUtc)> GenerateTokenAsync(User user)
        {
            var tokenExpiration = Startup.OAuthOptions.AccessTokenExpireTimeSpan;
            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Role, await GetRolesAsStringAsync(user)));

            var expires = DateTime.UtcNow.Add(tokenExpiration);
            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = expires,
            };
            var ticket = new AuthenticationTicket(identity, props);
            var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
            return (accessToken, expires, DateTime.UtcNow);
        }

        private async Task<string> GetRolesAsStringAsync(User user)
        {
            return string.Join(",", await _userManager.GetRolesAsync(user.Id));
        }
    }
}
