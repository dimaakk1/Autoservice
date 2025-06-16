using Autoservice.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Record> Records { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Records)
                .HasForeignKey(r => r.ClientId);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Records)
                .HasForeignKey(r => r.CarId);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Service)
                .WithMany(s => s.Records)
                .HasForeignKey(r => r.ServiceId);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Services)
                .HasForeignKey(s => s.EmployeeId);

            modelBuilder.Entity<RefreshToken>()
                   .HasOne(r => r.User)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(r => r.UserId);
        }
    }
}
