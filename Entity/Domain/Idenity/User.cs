using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts;
using Entity.Domain.Training;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Entity.Domain.Identity
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, ITimeStamp, IEntity
    {
        public User()
        {
            CreatedUtc = DateTime.UtcNow;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
        /// <summary>
        /// Время регистрации пользоваеля
        /// </summary>
        public DateTime CreatedUtc { get; set; }
        /// <summary>
        /// Настройки пользователя
        /// </summary>
        public Settings.Settings Settings { get; set; }
        /// <summary>
        /// Список тренировок пользователя
        /// </summary>
        public List<UserTraining> Trainings { get; set; }

 
    }
}