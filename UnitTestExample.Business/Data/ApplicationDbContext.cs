using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using UnitTestExample.Business.Models;

namespace UnitTestExample.Business.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :
            base(options)
        { }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasKey(p => p.TripId);
            modelBuilder.Entity<User>()
                .HasKey(p => p.UserId);
            modelBuilder.Entity<Bill>()
                .HasKey(p => p.BillId);
        }
    }

}
