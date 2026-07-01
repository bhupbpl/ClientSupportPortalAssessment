using ClientSupportPortal.Models;
using ClientSupportPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSupportPortal.Pages.Tickets;

/// <summary>
/// Razor Page model for displaying and filtering the list of support tickets.
/// </summary>
public class IndexModel(TicketService ticketService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? StatusFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? PriorityFilter { get; set; }

    public List<Ticket> Tickets { get; set; } = [];

    public int DisplayedCount { get; set; }

    public string[] StatusOptions => TicketConstants.Status.AllValues;

    public string[] PriorityOptions => TicketConstants.Priority.AllValues;

    /// <summary>
    /// Handles GET requests to display filtered tickets.
    /// Uses TicketService for efficient database-level filtering.
    /// </summary>
    public async Task OnGetAsync()
    {
        // Delegate to TicketService which handles all filtering logic correctly
        // and efficiently at the database level using IQueryable
        Tickets = await ticketService.GetFilteredTicketsAsync(
            SearchTerm,
            StatusFilter,
            PriorityFilter);

        DisplayedCount = Tickets.Count;
    }
}
