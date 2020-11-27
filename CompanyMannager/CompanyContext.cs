using CompanyMannager.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyMannager
{
    public sealed class CompanyContext: DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}