using System;
using CompanyMannager.Enums;
using CompanyMannager.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyMannager
{
    public sealed class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.JobTitle)
                .HasConversion(
                    e => (JobTitle)Enum.Parse(typeof(JobTitle), e),
                    e => e.ToString());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}