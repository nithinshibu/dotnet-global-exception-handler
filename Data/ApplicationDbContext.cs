using GlobalExceptionHandling.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandling.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


       public DbSet<Driver> Drivers { get; set; }



    }
}
