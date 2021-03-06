﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Implementations.Configurations;
using Data.Implementations.Migrations;
using Entity.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.Implementations.Context
{
    public class DataContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public DataContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // The Identity entities configuration canot be removed into a separate class configuration
            // as they would have overriden the pre-defined in-house configuration
            //
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasRequired(s=>s.Settings).WithRequiredPrincipal(s=>s.User).WillCascadeOnDelete(true);
            modelBuilder.Entity<User>().HasMany(s => s.Trainings).WithRequired(s => s.User).WillCascadeOnDelete(true);
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Configurations.Add(new CriteriaConfiguration());
            modelBuilder.Configurations.Add(new ExerciseConfiguration());
            modelBuilder.Configurations.Add(new ExerciseCriteriaConfiguration());
            modelBuilder.Configurations.Add(new ProfessionConfiguration());
            modelBuilder.Configurations.Add(new ProfessionCriteriaConfiguration());
            modelBuilder.Configurations.Add(new SettingsConfiguration());
            modelBuilder.Configurations.Add(new TrainingTimeConfiguration());
            modelBuilder.Configurations.Add(new UserTrainingConfiguration());
            modelBuilder.Configurations.Add(new UserExerciseConfiguration());

        }
    }
}
