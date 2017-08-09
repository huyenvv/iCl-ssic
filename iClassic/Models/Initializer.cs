using iClassic.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace iClassic.Models
{
    public class Initializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var hasher = new PasswordHasher();
            var pass = hasher.HashPassword("111111");


            context.Users.AddOrUpdate(
              p => p.Id,
              new ApplicationUser { UserName = "admin", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = "Lê Anh Tuấn"}
            );

            foreach (var roleName in RoleList.GetAll())
            {
                context.Roles.AddOrUpdate(
                    p => p.Id,
                        new IdentityRole
                        {
                            Name = roleName
                        }
                    );
            }
        }
    }
}