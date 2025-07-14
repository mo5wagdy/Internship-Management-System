using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public  class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<InternshipApplication> InternshipApplications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User Entity Configuration
            modelBuilder.Entity<User>(e =>
            {
                e.Property(u => u.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

                e.HasIndex(u => u.Email)
                    .IsUnique();

                e.Property(u => u.Role)
                .HasConversion<string>();
            });


            //Internship Entity Configuration
            modelBuilder.Entity<Internship>(e =>
            {
                e.Property(i => i.Title)
                    .IsRequired()
                    .HasMaxLength(100);
                e.Property(i => i.Description)
                    .HasMaxLength(1000);
                e.HasOne(i => i.Company)
                    .WithMany(u => u.internships)
                    .HasForeignKey(i => i.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //InternshipApplication Entity Configuration
            modelBuilder.Entity<InternshipApplication>(e =>
            {
                e.Property(a => a.Status)
                    .HasConversion<string>();
                e.HasOne(a => a.Student)
                    .WithMany(u => u.Applications )
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(a => a.Internship)
                    .WithMany(i => i.Applications)
                    .HasForeignKey(a => a.InternshipId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
