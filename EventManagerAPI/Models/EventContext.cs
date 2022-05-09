using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Models
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set;}
    }
}
