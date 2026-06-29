using ClientSupportPortal.Data;
using ClientSupportPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientSupportPortal.Services;

public class TicketService(AppDbContext context)
{
    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await context.Tickets.FindAsync(id);
    }

    public async Task<List<Ticket>> GetFilteredTicketsAsync(string? searchTerm, string? status, string? priority)
    {
        /*Improving performance, move filtering logic to the database query using IQueryable
             Filters execute on database server
             Only matching records are loaded into memory
             Scalable for large datasets
         */

        IQueryable<Ticket> query = context.Tickets;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim();
            query = query.Where(t =>
                EF.Functions.Like(t.Title!, $"%{search}%") ||
                EF.Functions.Like(t.Description!, $"%{search}%") ||
                EF.Functions.Like(t.RequesterName!, $"%{search}%") ||
                EF.Functions.Like(t.AssignedTo!, $"%{search}%"));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(t => t.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(priority))
        {
            query = query.Where(t => t.Priority == priority);
        }

        return await query.OrderByDescending(t => t.CreatedDate).ToListAsync();
    
    }

    public async Task AddAsync(Ticket ticket)
    {
        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    private static bool ContainsText(string? value, string search)
    {
        return value != null && value.Contains(search, StringComparison.OrdinalIgnoreCase);
    }
}
