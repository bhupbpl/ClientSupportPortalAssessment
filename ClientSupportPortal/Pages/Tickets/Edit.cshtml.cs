using ClientSupportPortal.Models;
using ClientSupportPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSupportPortal.Pages.Tickets;

public class EditModel(TicketService ticketService) : PageModel
{
    [BindProperty]
    public Ticket Ticket { get; set; } = new();

    // Dropdown options for form
    public string[] StatusOptions => TicketConstants.Status.AllValues;
    public string[] PriorityOptions => TicketConstants.Priority.AllValues;
    public string[] CategoryOptions => TicketConstants.Category.AllValues;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var ticket = await ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        Ticket = ticket;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Validate the model state
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

        var ticket = await ticketService.GetByIdAsync(Ticket.Id);

        if (ticket == null)
        {
            return NotFound();
        }

        ticket.Title = Ticket.Title;
        ticket.Description = Ticket.Description;
        ticket.RequesterName = Ticket.RequesterName;
        ticket.RequesterEmail = Ticket.RequesterEmail;
        ticket.Status = Ticket.Status;
        ticket.Priority = Ticket.Priority;
        ticket.Category = Ticket.Category;
        ticket.AssignedTo = Ticket.AssignedTo;
        ticket.LastUpdatedDate = DateTime.Now;

        await ticketService.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
