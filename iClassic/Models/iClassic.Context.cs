﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iClassic.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class iClassicEntities : DbContext
    {
        public iClassicEntities()
            : base("name=iClassicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<LoaiVai> LoaiVai { get; set; }
        public virtual DbSet<PhieuChi> PhieuChi { get; set; }
        public virtual DbSet<PhieuSanXuat> PhieuSanXuat { get; set; }
        public virtual DbSet<PhieuSua> PhieuSua { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<ProductTypeValue> ProductTypeValue { get; set; }
        public virtual DbSet<ProductTyeField> ProductTyeField { get; set; }
    }
}
