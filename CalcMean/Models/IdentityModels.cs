using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CalcMean.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;

namespace CalcMean.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class CmUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsShow { get; set; }

        public bool CoAnSang { get; set; }

        public virtual ICollection<DsNopTruoc> DsNopTruoc { get; set; }
        public virtual ICollection<UserInChiTieu> UserInChiTieu { get; set; }
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class CmContext : IdentityDbContext<CmUser>
    {
        public CmContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MyDbInitializer());
        }
        public DbSet<DsNopTruoc> DsNopTruoc { get; set; }
        public DbSet<UserInChiTieu> UserInChiTieu { get; set; }
        public DbSet<DsChiTieu> DsChiTieu { get; set; }
        public DbSet<DotChotSo> DotChotSo { get; set; }
        public DbSet<TienGao> TienGao { get; set; }

    }
}