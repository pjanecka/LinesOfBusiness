using LinesOfBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace LinesOfBusiness.Database
{
    public class GwpContext : DbContext
    {
        private static readonly string DatabaseName = "GwpDb";

        public DbSet<Gwp> Gwps { get; set; }
        public DbSet<GwpValue> GwpValues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(DatabaseName);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
