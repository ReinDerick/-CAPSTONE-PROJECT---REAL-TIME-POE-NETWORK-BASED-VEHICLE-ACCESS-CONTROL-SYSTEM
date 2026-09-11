using BsuAdmin.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BsuAdmin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {       
        }

        public DbSet<AddNewPlate> NewPlates { get; set; }

        public DbSet<AdminRegister> Admins { get; set; }

        public DbSet<AdminLogIn> adminLogIn { get; set; }

        public DbSet<CheckPlates> checkPlates { get; set; }
    }
}
