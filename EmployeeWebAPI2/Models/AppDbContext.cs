

using Microsoft.EntityFrameworkCore;

namespace EmployeeWebAPI2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Departement> Departements { get; set; }
    }
}
