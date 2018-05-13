using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Data.Implementations.Context;
using Entity.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace WebUI.Identity
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        private readonly DataContext _context;

        //public override async Task<IdentityResult> AddToRoleAsync(int userId, string role)
        //{
        //    var userExist = await _context.Set<User>().AnyAsync(s => s.Id == userId);
        //    if (!userExist)
        //    {
        //        return IdentityResult.Failed("User doesn't exist");
        //    }
        //    var roleInDb = await _context.Set<Role>().FirstOrDefaultAsync(s => s.Name.ToUpper() == role);
        //    if (roleInDb == null)
        //    {
        //        return IdentityResult.Failed("Role doesn't exist");
        //    }
        //    _context.Set<UserRole>().Add(new UserRole { UserId = userId, RoleId = roleInDb.Id });
        //    await _context.SaveChangesAsync();
        //    return IdentityResult.Success;    
        //}

        public ApplicationUserManager(DataContext dataContext, IdentityFactoryOptions<ApplicationUserManager> options)
            : base(new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(dataContext))
        {
            _context = dataContext;
            UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            EmailService = new EmailService();
            SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }
}