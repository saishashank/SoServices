﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoServices.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ServiceOrderDbEntities : DbContext
    {
        public ServiceOrderDbEntities()
            : base("name=ServiceOrderDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<StatusLookup> StatusLookups { get; set; }
        public virtual DbSet<DealerInformation> DealerInformations { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<ServicesProvided> ServicesProvideds { get; set; }
        public virtual DbSet<TransmissionLookup> TransmissionLookups { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserScheduleDetail> UserScheduleDetails { get; set; }
        public virtual DbSet<UserServiceOrderDetail> UserServiceOrderDetails { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
    }
}
