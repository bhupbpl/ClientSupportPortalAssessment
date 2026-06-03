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
        var tickets = await context.Tickets
            .OrderBy(t => t.CreatedDate)
            .ToListAsync();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim();
            tickets = tickets
                .Where(t =>
                    ContainsText(t.Title, search)
                    || ContainsText(t.Description, search)
                    || ContainsText(t.RequesterName, search)
                    || ContainsText(t.AssignedTo, search))
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            tickets = tickets
                .Where(t => t.Status == status)
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(priority))
        {
            tickets = tickets
                .Where(t => t.Priority == priority)
                .ToList();
        }

        return tickets;
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
