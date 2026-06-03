using ClientSupportPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientSupportPortal.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Ticket> Tickets => Set<Ticket>();
}
