using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkDetails> WorkDetails { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<WorkStatuses> WorkStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(r => r.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Report>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(k => k.GeneratedBy);

            modelBuilder.Entity<Worker>()
                .HasOne(u => u.Work)
                .WithMany()
                .HasForeignKey(k => k.WorkId);

            modelBuilder.Entity<Worker>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(k => k.EmployeeId);

            modelBuilder.Entity<WorkDetails>()
                .HasOne(u => u.Work)
                .WithMany()
                .HasForeignKey(k => k.WorkId);

            modelBuilder.Entity<WorkDetails>()
                .HasOne(u => u.Product)
                .WithMany()
                .HasForeignKey(k => k.ProductId);

            modelBuilder.Entity<Work>()
                .HasOne(m => m.workStatuses)
                .WithMany()
                .HasForeignKey(k => k.StatusId);

            modelBuilder.Entity<Notification>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(o => o.EmployeeId);

            modelBuilder.Entity<WorkNote>()
                .HasOne(l => l.Work)
                .WithMany()
                .HasForeignKey(k => k.WorkId);

            modelBuilder.Entity<WorkNote>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(k => k.EmployeeId);
        }
    }
}
