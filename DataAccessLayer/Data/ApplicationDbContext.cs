using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
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

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("1D460004-DFDD-4C24-8F60-CB52299A4096"),
                    RoleName = "Manager"
                },
                new Role
                {
                    Id = Guid.Parse("077FF91C-7598-416E-9AF9-1B484B85D410"),
                    RoleName = "Employee"
                }
            );

            modelBuilder.Entity<WorkStatuses>().HasData(
                new WorkStatuses
                {
                    Id = Guid.Parse("56D9991C-ACAE-491C-B90C-513959F938D3"),
                    StatusName = "في الانتظار"
                },
                new WorkStatuses
                {
                    Id = Guid.Parse("B15ACC43-4B04-451E-AFBB-EF09C1C8370A"),
                    StatusName = "جاري العمل عليه"
                },
                new WorkStatuses
                {
                    Id = Guid.Parse("31E38835-8793-494F-944B-74F0853A9A38"),
                    StatusName = "جاهز للأستلام"
                }
            );

            var password = new PasswordHasher<User>();
            var hashPassword = password.HashPassword(null, "Mm@7471009");

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("6FEB7AE5-BD2F-48EC-B004-FCE99CC30F8E"),
                Username = "SNR111",
                Email = "shopsnr111@gmail.com",
                Password = hashPassword,
                IsActive = true,
                RoleId = Guid.Parse("1D460004-DFDD-4C24-8F60-CB52299A4096"),
            });

        }
    }
}
