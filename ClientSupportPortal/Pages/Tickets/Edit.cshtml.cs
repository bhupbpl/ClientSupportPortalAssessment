using ClientSupportPortal.Models;
using ClientSupportPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSupportPortal.Pages.Tickets;

public class EditModel(TicketService ticketService) : PageModel
{
    [BindProperty]
    public Ticket Ticket { get; set; } = new();

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
