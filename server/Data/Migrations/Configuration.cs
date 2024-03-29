using Data.Implementations.Context;
using Microsoft.AspNet.Identity;

namespace Data.Implementations.Migrations
{
    using Entity.Domain.Identity;
    using Entity.Domain.Settings;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "UploaderDbContext";
        }

        protected override void Seed(DataContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new Role
                {
                    Name = "User"
                });

                var adminRole = new Role
                {
                    Name = "Admin"

                };
                context.Roles.Add(adminRole);

                var hasher = new PasswordHasher();
                var userAdmin = new User
                {
                    Email = "admin@admin.org",
                    PhoneNumber = "123-123-123",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword("admin"),
                    UserName = "admin",
                    Settings = new Settings()
                };
                context.Users.Add(userAdmin);
                context.SaveChanges();

                var userRoleSet = context.Set<UserRole>();
                userRoleSet.AddOrUpdate(new UserRole
                {
                    RoleId = adminRole.Id,
                    UserId = userAdmin.Id
                });
                context.SaveChanges();
            }

            base.Seed(context);
        }
    }
}
