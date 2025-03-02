using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AirCanadaApp.Models;

namespace AirCanadaApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AirCanadaApp.Models.FlightData> FlightData { get; set; } = default!;
        public DbSet<AirCanadaApp.Models.TicketOrder> TicketOrder { get; set; } = default!;
    }
}
