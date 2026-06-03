using ClientSupportPortal.Models;
using ClientSupportPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSupportPortal.Pages.Tickets;

public class CreateModel(TicketService ticketService) : PageModel
{
    [BindProperty]
    public Ticket Ticket { get; set; } = new();

    public void OnGet()
    {
        Ticket.Status = "Open";
        Ticket.Priority = "Medium";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Ticket.CreatedDate = DateTime.Now;
        Ticket.LastUpdatedDate = DateTime.Now;

        await ticketService.AddAsync(Ticket);

        return RedirectToPage("./Index");
    }
}
