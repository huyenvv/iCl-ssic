using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using CalcMean.Models;
using Microsoft.AspNet.Identity;

namespace CalcMean.Migrations
{
    public class MyDbInitializer : CreateDatabaseIfNotExists<CmContext>
    {
        protected override void Seed(CmContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var hasher = new PasswordHasher();
            var pass = hasher.HashPassword("111111");


            context.Users.AddOrUpdate(
              p => p.Id,
              new CmUser { UserName = "ChinhDD", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = Common.Encode("Đỗ Đức Chính"), CreateDate = DateTime.Now, PhoneNumber = "0979999899", IsShow = true, CoAnSang = true },
              new CmUser { UserName = "XuyenHTK", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = Common.Encode("Kim Xuyến"), CreateDate = DateTime.Now, PhoneNumber = "0979999899", IsShow = true, CoAnSang = true },
              new CmUser { UserName = "HuyenVV", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = Common.Encode("Huyền Vũ"), CreateDate = DateTime.Now, PhoneNumber = "0973561921", IsShow = true },
              new CmUser { UserName = "HoaiNK", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = Common.Encode("Khắc Hoài"), CreateDate = DateTime.Now, PhoneNumber = "0979999899", IsShow = true, CoAnSang = true },
              new CmUser { UserName = "HongTD", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = Common.Encode("Hồng Trần"), CreateDate = DateTime.Now, PhoneNumber = "0979999899", IsShow = true, CoAnSang = true },
              new CmUser { UserName = "HoanNC", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = Common.Encode("Công Hoan"), CreateDate = DateTime.Now, PhoneNumber = "0979999899", IsShow = true },
              new CmUser { UserName = "MinhNH", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = Common.Encode("Minh Đệ"), CreateDate = DateTime.Now, PhoneNumber = "0979999899", IsShow = true },
              new CmUser { UserName = "Hang", SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = pass, Name = Common.Encode("Hằng"), CreateDate = DateTime.Now, PhoneNumber = "0979999899", IsShow = true }
            );
        }
    }
}

