using ClientSupportPortal.Data;
using ClientSupportPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ClientSupportPortal.Pages.Tickets;

public class IndexModel(AppDbContext context) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? StatusFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? PriorityFilter { get; set; }

    public List<Ticket> Tickets { get; set; } = [];

    public int DisplayedCount { get; set; }

    public string[] StatusOptions { get; } = ["Open", "In Progress", "Closed"];

    public string[] PriorityOptions { get; } = ["Low", "Medium", "High", "Critical"];

    public async Task OnGetAsync()
    {
        var tickets = await context.Tickets
            .OrderBy(t => t.CreatedDate)
            .ToListAsync();

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            var search = SearchTerm.Trim();

            tickets = tickets
                .Where(t =>
                    ContainsText(t.Title, search)
                    || ContainsText(t.Description, search)
                    || ContainsText(t.RequesterName, search) && (string.IsNullOrWhiteSpace(StatusFilter) || t.Status == StatusFilter)
                    || ContainsText(t.AssignedTo, search)
                    || t.AssignedTo == string.Empty && search.Equals("unassigned", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(StatusFilter) && string.IsNullOrWhiteSpace(SearchTerm))
        {
            tickets = tickets
                .Where(t => t.Status == StatusFilter)
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(PriorityFilter))
        {
            tickets = tickets
                .Where(t => t.Priority == PriorityFilter)
                .ToList();
        }

        Tickets = tickets;
        DisplayedCount = Tickets.Count;
    }

    private static bool ContainsText(string? value, string search)
    {
        return value != null && value.Contains(search, StringComparison.OrdinalIgnoreCase);
    }
}
