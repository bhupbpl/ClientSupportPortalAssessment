using ClientSupportPortal.Models;
using ClientSupportPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSupportPortal.Pages.Tickets;

public class DetailsModel(TicketService ticketService) : PageModel
{
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
}
