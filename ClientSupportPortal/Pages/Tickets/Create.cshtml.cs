using ClientSupportPortal.Models;
using ClientSupportPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSupportPortal.Pages.Tickets;

public class CreateModel(TicketService ticketService) : PageModel
{
    [BindProperty]
    public Ticket Ticket { get; set; } = new();

    // Dropdown options for form
    public string[] StatusOptions => TicketConstants.Status.AllValues;
    public string[] PriorityOptions => TicketConstants.Priority.AllValues;
    public string[] CategoryOptions => TicketConstants.Category.AllValues;

    public void OnGet()
    {
        // Set sensible defaults for new tickets
        Ticket.Status = TicketConstants.Status.Open;
        Ticket.Priority = TicketConstants.Priority.Medium;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Model validation is automatically performed by Razor Pages when the form is submitted.
        // If the model state is invalid, the page will be redisplayed with validation messages.
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Additional server-side validation as defense-in-depth
        if (!TicketConstants.Status.IsValid(Ticket.Status))
        {
            ModelState.AddModelError("Ticket.Status", "Invalid status value.");
            return Page();
        }

        if (!TicketConstants.Priority.IsValid(Ticket.Priority))
        {
            ModelState.AddModelError("Ticket.Priority", "Invalid priority value.");
            return Page();
        }

        if (!TicketConstants.Category.IsValid(Ticket.Category))
        {
            ModelState.AddModelError("Ticket.Category", "Invalid category value.");
            return Page();
        }

        Ticket.CreatedDate = DateTime.Now;
        Ticket.LastUpdatedDate = DateTime.Now;

        await ticketService.AddAsync(Ticket);

        return RedirectToPage("./Index");
    }
}
