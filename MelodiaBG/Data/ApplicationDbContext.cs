using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MelodiaBG.Models;
using Microsoft.AspNetCore.Identity; // Добави namespace за моделите, ако е необходимо

namespace MelodiaBG.Data
{
    public class ApplicationDbContext : IdentityDbContext<User> // Укажи разширения User клас
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Добавяне на DbSet за допълнителните таблици

        public DbSet<IdentityUser> IdentityUsers { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Базова конфигурация на Identity
            base.OnModelCreating(modelBuilder);

            // Допълнителна Fluent API конфигурация (ако е необходимо)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Instrument)
                .WithMany()
                .HasForeignKey(r => r.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Instrument)
                .WithMany()
                .HasForeignKey(od => od.InstrumentId);
        }
    }
}

