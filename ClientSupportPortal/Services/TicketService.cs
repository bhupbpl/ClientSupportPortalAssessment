using ClientSupportPortal.Data;
using ClientSupportPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientSupportPortal.Services;

/// <summary>
/// Service for managing ticket operations including filtering, retrieval, and persistence.
/// </summary>
public class TicketService(AppDbContext context)
{
    /// <summary>
    /// Retrieves a single ticket by its ID.
    /// </summary>
    /// <param name="id">The ticket ID.</param>
    /// <returns>The ticket if found, otherwise null.</returns>
    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await context.Tickets.FindAsync(id);
    }

    /// <summary>
    /// Retrieves a filtered list of tickets based on search criteria.
    /// All filtering is performed at the database level for optimal performance.
    /// </summary>
    /// <param name="searchTerm">Optional search term to match against title, description, requester name, and assignee.</param>
    /// <param name="status">Optional status filter (e.g., "Open", "In Progress", "Closed").</param>
    /// <param name="priority">Optional priority filter (e.g., "Low", "Medium", "High", "Critical").</param>
    /// <returns>A list of tickets matching the filter criteria, ordered by creation date (newest first).</returns>
    public async Task<List<Ticket>> GetFilteredTicketsAsync(string? searchTerm, string? status, string? priority)
    {
        /* Performance optimization: Use IQueryable for database-level filtering
         * - Filters execute on the database server (SQL WHERE clauses)
         * - Only matching records are loaded into memory
         * - Scalable for large datasets (thousands of tickets)
         * - Reduces network traffic and memory consumption
         */

        IQueryable<Ticket> query = context.Tickets;

        // Apply search term filter if provided
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim();

            // Special handling for "unassigned" search
            var isUnassignedSearch = search.Equals("unassigned", StringComparison.OrdinalIgnoreCase);

            query = query.Where(t =>
                // Search in title
                EF.Functions.Like(t.Title!, $"%{search}%") ||
                // Search in description
                EF.Functions.Like(t.Description!, $"%{search}%") ||
                // Search in requester name
                EF.Functions.Like(t.RequesterName!, $"%{search}%") ||
                // Search in assignee (handles null safely)
                (t.AssignedTo != null && EF.Functions.Like(t.AssignedTo, $"%{search}%")) ||
                // Special case: search for "unassigned" matches null/empty AssignedTo
                (isUnassignedSearch && (t.AssignedTo == null || t.AssignedTo == "")));
        }

        // Apply status filter if provided (works independently or combined with search)
        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(t => t.Status == status);
        }

        // Apply priority filter if provided (works independently or combined with other filters)
        if (!string.IsNullOrWhiteSpace(priority))
        {
            query = query.Where(t => t.Priority == priority);
        }

        // Return results ordered by newest first
        return await query
            .OrderByDescending(t => t.CreatedDate)
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new ticket to the database.
    /// </summary>
    /// <param name="ticket">The ticket to add.</param>
    public async Task AddAsync(Ticket ticket)
    {
        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Saves all pending changes to the database.
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
