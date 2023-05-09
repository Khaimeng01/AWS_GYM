using Assigment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assigment.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<gymSession> GymSessions { get; set; }

        public DbSet<bookingSession> BookingSessions { get; set; }
    }
}