using Microsoft.EntityFrameworkCore;
using UminoWeb.DAL.Entities;

namespace UminoWeb.DAL.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
