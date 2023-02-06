#nullable disable

using Microsoft.EntityFrameworkCore;

namespace Notissimus_Task.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Models.MusicOffer> musicOffers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
